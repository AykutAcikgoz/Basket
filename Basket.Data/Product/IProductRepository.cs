using System;
using System.Net;

namespace Basket.Data.Product
{
    public interface IProductRepository : IRepository<Entity.Product, string>
    {
    }
}

