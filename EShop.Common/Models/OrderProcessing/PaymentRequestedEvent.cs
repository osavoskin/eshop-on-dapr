using System;

namespace EShop.Common.Models.OrderProcessing
{
    public class PaymentRequestedEvent
    {
        public Guid OrderId { get; set; }

        public decimal Sum { get; set; }
    }
}
