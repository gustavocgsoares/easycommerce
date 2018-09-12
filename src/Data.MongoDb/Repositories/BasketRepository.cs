using System;
using Easy.Commerce.CrossCutting;
using Easy.Commerce.Data.MongoDb.Repositories.Base;
using Easy.Commerce.Domain.Entities;
using Easy.Commerce.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Options;

namespace Easy.Commerce.Data.Repositories.Corporate
{
    public class BasketRepository
        : MongoDbRepository<Basket, Guid>, IBasketRepository
    {
        public BasketRepository(IOptions<ConnectionStrings> connectionString)
            : base(connectionString.Value?.DefaultConnection, "basket")
        {
        }
    }
}
