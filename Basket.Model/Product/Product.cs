using System;
namespace Basket.Entity
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public string Category { get; set; }
        public int AvailableQuantity { get; set; }
        public decimal Price { get; set; }
    }
}

