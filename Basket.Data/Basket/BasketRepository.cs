using System;
using Basket.Data.Product;

namespace Basket.Data.Basket
{
    public class BasketRepository : Repository<Entity.Basket.Basket>, IBasketRepository
    {
        public BasketRepository(MongoDbContext context) : base(context)
        {
        }
    }
}

