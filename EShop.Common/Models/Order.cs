using System;
using System.Collections.Generic;

namespace EShop.Common.Models
{
    public class Order
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public IDictionary<string, int> Items { get; set; }
    }
}
