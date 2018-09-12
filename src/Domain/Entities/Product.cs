using System;
using System.Collections.Generic;
using Easy.Commerce.Domain.Enums;
using Easy.Commerce.Domain.Shared;

namespace Easy.Commerce.Domain.Entities
{
    public class Product : Entity<Product, Guid>
    {
        public virtual string Sku { get; set; }

        public virtual string Name { get; set; }

        public virtual short Quantity { get; set; }

        public virtual ICollection<string> Images { get; set; }

        public virtual CurrencyIso Currency { get; set; }

        public virtual decimal UnityValue { get; set; }

        public virtual decimal Total { get; set; }
    }
}
