using Easy.Commerce.Domain.Shared;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;

namespace Easy.Commerce.Data.MongoDb.Helpers
{
    public static class ClassMapHelper
    {
        private static object lockObject = new object();

        public static void RegisterConventionPacks()
        {
            lock (lockObject)
            {
                var conventionPack = new ConventionPack();

                conventionPack.Add(new IgnoreIfNullConvention(true));
                conventionPack.Add(new EnumRepresentationConvention(BsonType.String));
                conventionPack.Add(new CamelCaseElementNameConvention());

                ConventionRegistry.Register("ConventionPack", conventionPack, t => true);
            }
        }

        public static void SetupClassMap<TEntity, TId>()
            where TEntity : Entity<TEntity, TId>
        {
            lock (lockObject)
            {
                if (!BsonClassMap.IsClassMapRegistered(typeof(TEntity)))
                {
                    BsonClassMap.RegisterClassMap<TEntity>(
                        (classMap) =>
                        {
                            classMap.AutoMap();
                            classMap.SetIdMember(classMap.GetMemberMap(a => a.Id));
                            classMap.SetDiscriminator(typeof(TEntity).Name);
                            classMap.SetIgnoreExtraElementsIsInherited(true);
                        });
                }
            }
        }
    }
}