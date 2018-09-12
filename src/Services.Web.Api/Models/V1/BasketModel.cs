using System;
using System.Collections.Generic;
using Easy.Commerce.Domain.Enums;
using Easy.Commerce.Services.Web.Api.Models.Shared;

namespace Easy.Commerce.Services.Web.Api.Models.V1
{
    /// <summary>
    /// Define the basket of products.
    /// </summary>
    [Serializable]
    public class BasketModel : BaseModel<BasketModel>
    {
        /// <summary>
        /// The basket id.
        /// </summary>
        public virtual Guid Id { get; set; }

        /// <summary>
        /// Items added in the basket.
        /// </summary>
        public virtual ICollection<ProductModel> Items { get; set; }

        /// <summary>
        /// Currency for total value in the basket.
        /// </summary>
        public virtual CurrencyIso Currency { get; set; }

        /// <summary>
        /// Total basket value.
        /// </summary>
        public virtual decimal Total { get; set; }
    }
}