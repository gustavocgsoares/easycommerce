using System;
using System.Collections.Generic;
using Easy.Commerce.Services.Web.Api.Models.Shared;
using Easy.Commerce.Services.Web.Api.Models.V1.Enums;

namespace Easy.Commerce.Services.Web.Api.Models.V1
{
    /// <summary>
    /// Define a product data.
    /// </summary>
    [Serializable]
    public class ProductModel : BaseModel<ProductModel>
    {
        /// <summary>
        /// Product code.
        /// </summary>
        public virtual string Sku { get; set; }

        /// <summary>
        /// Product name.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Quantity of product in the basket.
        /// </summary>
        public virtual short Quantity { get; set; }

        /// <summary>
        /// Product images.
        /// </summary>
        public virtual ICollection<string> Images { get; set; }

        /// <summary>
        /// Currency iso for value.
        /// </summary>
        public virtual CurrencyIso Currency { get; set; }

        /// <summary>
        /// Unity value.
        /// </summary>
        public virtual decimal UnityValue { get; set; }

        /// <summary>
        /// Total value (Quantity * UnityValue).
        /// </summary>
        public virtual decimal Total { get; set; }
    }
}