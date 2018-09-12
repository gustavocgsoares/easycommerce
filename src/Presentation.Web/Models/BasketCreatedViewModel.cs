using System;
using System.ComponentModel;

namespace Easy.Commerce.Presentation.Web.Models
{
    public class BasketCreatedViewModel
    {
        [DisplayName("Id")]
        public virtual Guid Id { get; set; }

        [DisplayName("Sku")]
        public virtual string Sku { get; set; }

        [DisplayName("Nome")]
        public virtual string Name { get; set; }

        [DisplayName("Quantidade")]
        public virtual short Quantity { get; set; }

        [DisplayName("Url da Imagem")]
        public virtual string Image { get; set; }

        [DisplayName("Moeda")]
        public virtual CurrencyIso Currency { get; set; }

        [DisplayName("Valor Unitário")]
        public virtual decimal UnityValue { get; set; }

        [DisplayName("Total")]
        public virtual decimal Total { get; set; }
    }
}