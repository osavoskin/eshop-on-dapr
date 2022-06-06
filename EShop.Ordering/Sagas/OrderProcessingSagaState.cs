using System;
using System.Collections.Generic;

namespace EShop.Ordering.Sagas
{
    public class OrderProcessingSagaState
    {
        public string CurrentState { get; set; }

        public Guid OrderId { get; set; }

        public IDictionary<string, int> Items { get; set; }

        public decimal Sum { get; set; }
    }
}
