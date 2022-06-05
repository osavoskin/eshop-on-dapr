using System;
using System.Collections.Generic;
using System.Linq;

namespace EShop.Common.Models.OrderProcessing
{
    public class StockCheckedEvent
    {
        public IEnumerable<Guid> MissingItems { get; set; }

        public bool CanProceedeWithOrder
        {
            get => MissingItems?.Any() == true;
        }
    }
}
