using System;
using System.ComponentModel;

namespace Easy.Commerce.Presentation.Web.Models
{
    [Serializable]
    public class BasketRemoveItemViewModel
    {
        [DisplayName("Id")]
        public virtual Guid Id { get; set; }

        [DisplayName("Quantidade de Itens")]
        public virtual int ItemsQuantity { get; set; }

        [DisplayName("Moeda")]
        public virtual CurrencyIso Currency { get; set; }

        [DisplayName("Total")]
        public virtual decimal Total { get; set; }

        [DisplayName("Sku")]
        public virtual string Sku { get; set; }
    }
}