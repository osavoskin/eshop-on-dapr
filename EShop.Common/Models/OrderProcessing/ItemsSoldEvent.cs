using System;
using System.Collections.Generic;

namespace EShop.Common.Models.OrderProcessing
{
    public class ItemsSoldEvent
    {
        public IDictionary<Guid, int> Items { get; set; }
    }
}
