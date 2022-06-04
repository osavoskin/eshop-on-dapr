using EShop.Common.Models;

namespace EShop.WebApp.Models
{
    public class CatalogItem : Item
    {
        public bool IsChecked { get; set; }

        public int Count { get; set; }
    }
}
