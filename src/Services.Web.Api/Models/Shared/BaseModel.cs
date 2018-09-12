using AutoMapper;
using Easy.Commerce.Domain.Shared;

namespace Easy.Commerce.Services.Web.Api.Models.Shared
{
    /// <summary>
    /// Base model.
    /// </summary>
    /// <typeparam name="TModel">Type of model.</typeparam>
    public abstract class BaseModel<TModel>
        where TModel : new()
    {
        /// <summary>
        /// Get instance of model.
        /// </summary>
        /// <returns>Instance of model.</returns>
        public static TModel Instance()
        {
            return new TModel();
        }

        /// <summary>
        /// Convert <paramref name="entity"/> to <typeparamref name="TModel"/>.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity.</typeparam>
        /// <param name="entity">Entity <typeparamref name="TEntity"/> to be mapped.</param>
        /// <param name="mapper">See <see cref="IMapper"/>.</param>
        /// <returns>See <typeparamref name="TModel"/>.</returns>
        public static TModel ToModel<TEntity>(TEntity entity, IMapper mapper)
            where TEntity : BaseEntity
        {
            var model = mapper.Map<TEntity, TModel>(entity);
            return model;
        }

        /// <summary>
        /// Convert <typeparamref name="TModel"/> to <typeparamref name="TEntity"/>.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity.</typeparam>
        /// <param name="mapper">See <see cref="IMapper"/>.</param>
        /// <returns>See <typeparamref name="TEntity"/>.</returns>
        public virtual TEntity ToDomain<TEntity>(IMapper mapper)
            where TEntity : BaseEntity, new()
        {
            return ToDomain(new TEntity(), mapper);
        }

        /// <summary>
        /// Convert <typeparamref name="TModel"/> to <typeparamref name="TEntity"/>.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity.</typeparam>
        /// <param name="entity">Item <typeparamref name="TEntity"/> to be completed.</param>
        /// <param name="mapper">See <see cref="IMapper"/>.</param>
        /// <returns>See <typeparamref name="TEntity"/>.</returns>
        public virtual TEntity ToDomain<TEntity>(TEntity entity, IMapper mapper)
            where TEntity : BaseEntity, new()
        {
            entity = entity ?? new TEntity();
            entity = mapper.Map(this, entity);

            return entity;
        }
    }
}