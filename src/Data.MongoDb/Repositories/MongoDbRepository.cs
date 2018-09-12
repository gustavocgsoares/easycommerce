using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Authentication;
using System.Threading.Tasks;
using Easy.Commerce.Data.MongoDb.Helpers;
using Easy.Commerce.Domain.Interfaces.Repositories;
using Easy.Commerce.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Newtonsoft.Json;
using Polly;

namespace Easy.Commerce.Data.MongoDb.Repositories.Base
{
    public abstract class MongoDbRepository<TEntity, TId> : IRepository<TEntity, TId>
        where TEntity : Entity<TEntity, TId>
    {
        private static IMongoClient client;

        private static IMongoDatabase database;

        private Polly.Retry.RetryPolicy policy;

        private Polly.Retry.RetryPolicy policyAsync;

        private string collectionName;

        static MongoDbRepository()
        {
            ClassMapHelper.RegisterConventionPacks();
            ClassMapHelper.SetupClassMap<TEntity, TId>();
        }

        public MongoDbRepository(string connecionString, string collectionName)
        {
            this.collectionName = collectionName;

            policyAsync = ConfigureRetryPolicy(connecionString, true);
            policy = ConfigureRetryPolicy(connecionString, false);
            policy.Execute(() => InitiateMongoClient(connecionString, collectionName));
        }

        protected IMongoCollection<TEntity> Collection { get; set; }

        public virtual async Task<ServiceResponse<TEntity>> GetAsync(TId id)
        {
            var result = await policyAsync.ExecuteAsync(async () =>
            {
                return await Collection
                    .Find(e => e.Id.Equals(id))
                    .FirstOrDefaultAsync();
            });

            return new ServiceResponse<TEntity> { Result = result };
        }

        public virtual async Task<ServiceResponse<IEnumerable<TEntity>>> GetAsync(Expression<Func<TEntity, bool>> filter, params string[] includes)
        {
            var result = await policyAsync.ExecuteAsync(async () =>
            {
                return await Collection
                    .Find(filter)
                    .ToListAsync();
            });

            return new ServiceResponse<IEnumerable<TEntity>> { Result = result };
        }

        public IQueryable<TEntity> GetQueryable()
        {
            return Collection.AsQueryable();
        }

        public virtual async Task<ServiceResponse<TEntity>> SaveAsync(TEntity entity)
        {
            if (entity.IsTransient())
            {
                return await CreateAsync(entity);
            }
            else
            {
                return await UpdateAsync(entity);
            }
        }

        public async Task<ServiceResponse<bool>> DeleteAsync(TEntity entity)
        {
            await policyAsync.ExecuteAsync(async () =>
            {
                await Collection.FindOneAndDeleteAsync(e => e.Id.Equals(entity.Id));
            });

            return new ServiceResponse<bool> { Result = true };
        }

        public async Task<ServiceResponse<IEnumerable<TEntity>>> GetAsync(Expression<Func<TEntity, bool>> filter)
        {
            var result = await policyAsync.ExecuteAsync(async () =>
            {
                return await Collection.Find(filter)
                .ToListAsync();
            });

            return new ServiceResponse<IEnumerable<TEntity>> { Result = result };
        }

        public async Task<ServiceResponse<bool>> DeleteAsync(TId id)
        {
            await policyAsync.ExecuteAsync(async () =>
            {
                await Collection.FindOneAndDeleteAsync(e => e.Id.Equals(id));
            });

            return new ServiceResponse<bool> { Result = true };
        }

        public async Task<ServiceResponse<bool>> DeleteAsync(Expression<Func<TEntity, bool>> filter)
        {
            await policyAsync.ExecuteAsync(async () =>
            {
                await Collection.DeleteManyAsync(filter);
            });

            return new ServiceResponse<bool> { Result = true };
        }

        private async Task<ServiceResponse<TEntity>> CreateAsync(TEntity entity)
        {
            try
            {
                await policyAsync.ExecuteAsync(async () =>
                {
                    entity.Version = entity.Version.GetValueOrDefault() + 1;
                    await Collection.InsertOneAsync(entity, null);
                });

                return await GetAsync(entity.Id);
            }
            catch (MongoWriteException ex)
            {
                return new ServiceResponse<TEntity>
                {
                    Error = new Error
                    {
                        Exception = ex.Message,
                        Message = "Insert failed because the entity already exists!"
                    }
                };
            }
        }

        private async Task<ServiceResponse<TEntity>> UpdateAsync(TEntity entity)
        {
            var previuosVersion = entity.Version;
            entity.Version++;

            ReplaceOneResult result;

            //// -> Find entity with same Id
            var idFilter = Builders<TEntity>.Filter.Eq(e => e.Id, entity.Id);

            //// -> Consistency enforcement
            if (!IgnoreVersion())
            {
                var versionLowerThan = Builders<TEntity>.Filter.Lt(e => e.Version, entity.Version);

                //// -> Consistency enforcement: Where current._id = entity.Id AND entity.Version > current.Version
                result = await policyAsync.ExecuteAsync(async () =>
                {
                    return await Collection.ReplaceOneAsync(
                        Builders<TEntity>.Filter.And(idFilter, versionLowerThan),
                        entity,
                        null);
                });

                if (result != null && ((result.IsAcknowledged && result.MatchedCount == 0) || (result.IsModifiedCountAvailable && !(result.ModifiedCount > 0))))
                {
                    return new ServiceResponse<TEntity>
                    {
                        Error = new Error
                        {
                            Message = string.Format("Update failed because entity versions conflict! {0}", JsonConvert.SerializeObject(entity))
                        }
                    };
                }
            }
            else
            {
                result = await policyAsync.ExecuteAsync(async () =>
                {
                    return await Collection.ReplaceOneAsync(idFilter, entity, null);
                });

                if (result != null && ((result.IsAcknowledged && result.MatchedCount == 0) || (result.IsModifiedCountAvailable && !(result.ModifiedCount > 0))))
                {
                    return new ServiceResponse<TEntity>
                    {
                        Error = new Error
                        {
                            Message = string.Format("Data {0} not found.", entity.Id.ToString())
                        }
                    };
                }
            }

            return await GetAsync(entity.Id);
        }

        private Polly.Retry.RetryPolicy ConfigureRetryPolicy(string connectionString, bool isAsync)
        {
            if (isAsync)
            {
                return Policy.Handle<MongoConnectionException>()
                    .WaitAndRetryAsync(
                        5,
                        retryAttempt => TimeSpan.FromSeconds(1),
                        (exception, timeSpan, context) =>
                        {
                            InitiateMongoClient(connectionString, collectionName);
                        });
            }

            return Policy.Handle<MongoConnectionException>()
                .WaitAndRetry(
                    5,
                    retryAttempt => TimeSpan.FromSeconds(1),
                    (exception, timeSpan, context) =>
                    {
                        InitiateMongoClient(connectionString, collectionName);
                    });
        }

        private void InitiateMongoClient(string connectionString, string collectionName)
        {
            var mongoUrl = new MongoUrl(connectionString);
            var settings = MongoClientSettings.FromUrl(mongoUrl);

            settings.SslSettings = new SslSettings
            {
                EnabledSslProtocols = SslProtocols.Tls12,
            };

            client = new MongoClient(settings);
            database = client.GetDatabase(mongoUrl.DatabaseName);
            Collection = database.GetCollection<TEntity>(collectionName);
        }

        private bool IgnoreVersion()
        {
            return false;
        }
    }
}