using System;
using System.Threading.Tasks;
using Easy.Commerce.Domain.Shared;

namespace Easy.Commerce.Domain.Interfaces.Services
{
    public interface IBasketService
    {
        Task<ServiceResponse<Entities.Basket>> GetAsync(Guid basketId);

        Task<ServiceResponse<Entities.Basket>> CreateAsync();

        Task<ServiceResponse<Entities.Basket>> AddItemAsync(Guid basketId, Entities.Product item);

        Task<ServiceResponse<Entities.Basket>> RemoveItemAsync(Guid basketId, string productSku);
    }
}
