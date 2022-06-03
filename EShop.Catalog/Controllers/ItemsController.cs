using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Catalog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly Item[] items =
        {
            new Item
            { 
                Id = Guid.Parse("{0A922C8F-912F-4C9D-9D6B-015CD72587D6}"), 
                Title = "Socks", 
                Price = 1.5m
            },
            new Item
            { 
                Id = Guid.Parse("{C5B8A24D-1E2C-4515-89C0-ECF14A0F95A9}"), 
                Title = "Sweater", 
                Price = 10m
            },
            new Item
            { 
                Id = Guid.Parse("{F0AFC291-37BD-40FC-BD12-500A6FA0073C}"), 
                Title = "Jacket", 
                Price = 15.3m
            }
        };

        private readonly ILogger<ItemsController> _logger;

        public ItemsController(ILogger<ItemsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Item> Get() => items;
    }

    public class Item
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Title { get; set; }

        public decimal Price { get; set; }
    }
}
