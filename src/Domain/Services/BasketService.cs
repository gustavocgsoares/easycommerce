using System;
using System.Threading.Tasks;
using Easy.Commerce.Domain.Entities;
using Easy.Commerce.Domain.Interfaces.Repositories;
using Easy.Commerce.Domain.Interfaces.Services;
using Easy.Commerce.Domain.Shared;

namespace Easy.Commerce.Domain.Services
{
    public class BasketService : IBasketService
    {
        private const string BasketNotFound = "Basket not found.";
        private readonly IBasketRepository basketRepository;
        private ServiceResponse<Basket> responseError;

        public BasketService(IBasketRepository basketRepository)
        {
            this.basketRepository = basketRepository;
        }

        public async Task<ServiceResponse<Basket>> GetAsync(Guid basketId)
        {
            return await basketRepository.GetAsync(basketId);
        }

        public async Task<ServiceResponse<Basket>> CreateAsync()
        {
            return await basketRepository.SaveAsync(new Basket());
        }

        public async Task<ServiceResponse<Basket>> AddItemAsync(Guid basketId, Product item)
        {
            //// TODO: Item validation item?.ValidateAddToBasket();
            var response = await GetAsync(basketId);

            if ((responseError = CheckHasErrorOrNotFound(response)) != null)
            {
                return responseError;
            }

            var basket = response.Result;
            basket.AddItem(item);

            return await basketRepository.SaveAsync(response.Result);
        }

        public async Task<ServiceResponse<Basket>> RemoveItemAsync(Guid basketId, string productSku)
        {
            var response = await GetAsync(basketId);

            if ((responseError = CheckHasErrorOrNotFound(response)) != null)
            {
                return responseError;
            }

            var basket = response.Result;
            basket.RemoveItemBySku(productSku);

            return await basketRepository.SaveAsync(basket);
        }

        private ServiceResponse<Basket> CheckHasErrorOrNotFound(ServiceResponse<Basket> response)
        {
            if (response.HasError)
            {
                return response;
            }

            if (response.Result == null)
            {
                return new ServiceResponse<Basket>().WithErrorMessage(BasketNotFound);
            }

            return null;
        }
    }
}