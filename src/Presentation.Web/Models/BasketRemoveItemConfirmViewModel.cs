using System;
using System.ComponentModel;

namespace Easy.Commerce.Presentation.Web.Models
{
    [Serializable]
    public class BasketRemoveItemConfirmViewModel
    {
        [DisplayName("Id")]
        public virtual Guid Id { get; set; }

        [DisplayName("Sku")]
        public virtual string Sku { get; set; }
    }
}