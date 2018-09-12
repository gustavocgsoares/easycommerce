using System.Collections.Generic;
using System.Threading.Tasks;
using Easy.Commerce.Presentation.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;

namespace Easy.Commerce.Presentation.Web.Controllers
{
    public class BasketController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly RestClient client;

        public BasketController(IConfiguration configuration)
        {
            this.configuration = configuration;
            client = new RestClient(configuration["ApiEndpoint"]);
        }

        // GET: Basket
        public ActionResult Index()
        {
            return View();
        }

        // GET: Basket/Create
        public async Task<ActionResult> Create()
        {
            var request = new RestRequest("/v1/baskets", Method.POST);

            request.AddHeader("Content-Type", "application/json");
            request.RequestFormat = DataFormat.Json;
            var response = await client.ExecuteTaskAsync<object>(request);
            var content = JsonConvert.DeserializeObject<dynamic>(response?.Content);

            return View(new BasketCreatedViewModel { Id = content.id });
        }

        // POST: Basket/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddItem(BasketCreatedViewModel model)
        {
            try
            {
                var request = new RestRequest($"/v1/baskets/{model.Id}", Method.POST);

                request.AddHeader("Content-Type", "application/json");
                request.RequestFormat = DataFormat.Json;

                request.AddBody(new
                    {
                        sku = model.Sku,
	                    name = model.Name,
	                    quantity = model.Quantity,
	                    images = new List<string> { model.Image },
	                    currency = model.Currency,
	                    unityValue = model.UnityValue,
	                    total = model.Total,
                    });

                var response = await client.ExecuteTaskAsync<object>(request);
                var content = JsonConvert.DeserializeObject<dynamic>(response?.Content);

                var result = new BasketRemoveItemViewModel
                {
                    Id = content.id,
                    Currency = content.currency ?? CurrencyIso.BRL,
                    ItemsQuantity = content.items.Count,
                    Sku = model.Sku,
                    Total = content.total
                };

                return RedirectToAction(nameof(RemoveItem), result);
            }
            catch
            {
                return View();
            }
        }

        // GET: Basket
        public ActionResult RemoveItem(BasketRemoveItemViewModel model)
        {
            return View(model);
        }


        // POST: Basket/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveItem(BasketRemoveItemConfirmViewModel model)
        {
            try
            {
                var request = new RestRequest($"/v1/baskets/{model.Id}/items/{model.Sku}", Method.DELETE);

                request.AddHeader("Content-Type", "application/json");
                request.RequestFormat = DataFormat.Json;

                var response = await client.ExecuteTaskAsync<object>(request);

                return RedirectToAction(nameof(Congratulations));
            }
            catch
            {
                return View();
            }
        }

        // GET: Basket/Congratulations
        public ActionResult Congratulations()
        {
            return View();
        }
    }
}