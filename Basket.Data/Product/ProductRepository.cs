using System;
namespace Basket.Data.Product
{
    public class ProductRepository : Repository<Entity.Product>, IProductRepository
    {
        public ProductRepository(MongoDbContext context) : base(context)
        {
        }
    }
}

