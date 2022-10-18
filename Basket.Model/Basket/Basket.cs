using System;
namespace Basket.Entity.Basket
{
    public class Basket : BaseEntity
    {
        public string ClientId { get; set; }
        public List<BasketItem> Items { get; set; } = new List<BasketItem>();
    }
}

