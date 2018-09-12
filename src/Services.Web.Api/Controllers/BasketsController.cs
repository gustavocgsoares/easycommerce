using System;
using System.Threading.Tasks;
using AutoMapper;
using Easy.Commerce.Domain.Entities;
using Easy.Commerce.Domain.Interfaces.Services;
using Easy.Commerce.Domain.Services;
using Easy.Commerce.Services.Web.Api.Models.Shared;
using Easy.Commerce.Services.Web.Api.Models.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Easy.Commerce.Services.Web.Api.Controllers
{
    /// <summary>
    /// Basket APIs.
    /// </summary>
    [ApiVersion("1")]
    [Route("v{version:apiVersion}/baskets")]
    public class BasketsController : BaseApiController
    {
        /// <summary>
        /// Basket application flow.
        /// </summary>
        private readonly IBasketService basketService;

        /// <summary>
        /// See <see cref="IUrlHelperFactory"/>.
        /// </summary>
        private readonly IUrlHelperFactory urlHelperFactory;

        /// <summary>
        /// See <see cref="IMapper"/>.
        /// </summary>
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="BasketsController"/> class.
        /// </summary>
        /// <param name="basketService">See <see cref="BasketService"/>.</param>
        /// <param name="mapper">See <see cref="IMapper"/>.</param>
        /// <param name="urlHelperFactory">See <see cref="IUrlHelperFactory"/>.</param>
        public BasketsController(
            IBasketService basketService,
            IMapper mapper,
            IUrlHelperFactory urlHelperFactory)
            : base(mapper, urlHelperFactory)
        {
            this.basketService = basketService;
            this.mapper = mapper;
            this.urlHelperFactory = urlHelperFactory;
        }

        /// <summary>
        /// <b>GetBasketById:</b> Retrieves a specific basket by unique id.
        /// </summary>
        /// <param name="basketId">Basket id.</param>
        /// <response code="200">Basket found.</response>
        /// <response code="400">Basket has missing/invalid values.</response>
        /// <response code="404">Basket not found or not exists.</response>
        /// <response code="500">Oops! Can't get your basket right now.</response>
        /// <returns>Any status code and response as described.</returns>
        [HttpGet]
        [Route("{basketId}", Name = "GetBasketById")]
        [ProducesResponseType(typeof(BasketModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBasketById(Guid basketId)
        {
            var entity = await basketService.GetAsync(basketId);
            var model = BasketModel.ToModel(entity.Result, mapper);

            return Ok(model);
        }

        /// <summary>
        /// <b>CreateBasket:</b> Create a basket.
        /// </summary>
        /// <remarks>The basket will be created when an item is added!</remarks>
        /// <response code="201">Basket created.</response>
        /// <response code="400">Basket has missing/invalid values.</response>
        /// <response code="409">Basket has conflicting values with existing data.</response>
        /// <response code="500">Oops! Can't create your basket right now.</response>
        /// <returns>Any status code and response as described.</returns>
        [HttpPost]
        [Route("", Name = "CreateBasket")]
        [ProducesResponseType(typeof(BasketModel), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BadRequestModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateBasket()
        {
            ////item.IsNull().Throw<InvalidParameterException>(string.Format(Messages.CannotBeNull, "item"));

            var entity = await basketService.CreateAsync();
            var result = BasketModel.ToModel(entity.Result, mapper);

            var urlHelper = urlHelperFactory.GetUrlHelper(ControllerContext);

            return new CreatedResult(string.Empty, result);
        }

        /// <summary>
        /// <b>AddBasketItem:</b> Add item in the basket.
        /// To use this api you should have a basket.
        /// Otherwise you have to use CreateBasket api.
        /// </summary>
        /// <param name="basketId">Basket id to add item.</param>
        /// <param name="item">Item to be added.</param>
        /// <response code="200">Item added.</response>
        /// <response code="400">Basket has missing/invalid values.</response>
        /// <response code="404">Basket not found or not exists.</response>
        /// <response code="409">Basket has conflicting values with existing data.</response>
        /// <response code="500">Oops! Can't add item in the basket right now.</response>
        /// <returns>Any status code and response as described.</returns>
        [HttpPost]
        [Route("{basketId}", Name = "AddBasketItem")]
        [ProducesResponseType(typeof(BasketModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddBasketItem(Guid basketId, [FromBody]ProductModel item)
        {
            var entity = await basketService.AddItemAsync(basketId, item.ToDomain<Product>(mapper));
            var result = BasketModel.ToModel(entity.Result, mapper);

            return Ok(result);
        }

        /// <summary>
        /// <b>RemoveBasketItem:</b> Remove item from basket.
        /// </summary>
        /// <param name="basketId">Basket id.</param>
        /// <param name="sku">One or more item's id delimited by comma.</param>
        /// <remarks>If you don't pass ids, we will remove all basket items!</remarks>
        /// <response code="200">Items removed. Returns the basket without deleted items. \o/</response>
        /// <response code="400">Basket or item's id has missing/invalid values.</response>
        /// <response code="500">Oops! Can't remove your basket items right now.</response>
        /// <returns>Any status code and response as described.</returns>
        [HttpDelete]
        [Route("{basketId}/items/{sku}", Name = "RemoveBasketItem")]
        [ProducesResponseType(typeof(BasketModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveBasketItem(Guid basketId, string sku)
        {
            var entity = await basketService.RemoveItemAsync(basketId, sku);
            var result = BasketModel.ToModel(entity.Result, mapper);

            return Ok(result);
        }
    }
}
