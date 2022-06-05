using System;

namespace EShop.Common.Models.OrderProcessing
{
    public class OrderPayedEvent
    {
        public Guid OrderId { get; set; }

        public bool Success { get; set; }
    }
}
