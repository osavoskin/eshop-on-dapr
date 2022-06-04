﻿using System;

namespace EShop.Common.Models
{
    public class Item
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Title { get; set; }

        public decimal Price { get; set; }
    }
}
