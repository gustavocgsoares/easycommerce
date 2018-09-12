using System;
using System.Collections.Generic;
using System.Linq;
using Easy.Commerce.Domain.Enums;
using Easy.Commerce.Domain.Shared;

namespace Easy.Commerce.Domain.Entities
{
    public class Basket : Entity<Basket, Guid>
    {
        public Basket()
        {
            Items = new HashSet<Product>();
        }

        public virtual ICollection<Product> Items { get; set; }

        public virtual CurrencyIso Currency
        {
            get
            {
                return Items?.FirstOrDefault()?.Currency ?? CurrencyIso.BRL;
            }
        }

        public virtual decimal Total { get; set; }

        internal void RemoveItemBySku(string productSku)
        {
            Items = Items.Where(i => !i.Sku.Equals(productSku, StringComparison.InvariantCultureIgnoreCase)).ToList();
            Total = Items.Sum(i => i.Total);
        }

        internal void AddItem(Product item)
        {
            Items.Add(item);
            Total = Items.Sum(i => i.Total);
        }
    }
}