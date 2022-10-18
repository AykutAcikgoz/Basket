using System;
using Basket.Service.Payload;

namespace Basket.Service.Product
{
    public interface IProductService
    {
        ResponsePayload GetAllProducts();
        Task<ResponsePayload> GetProductByIdAsync(string id);
        Task<bool> UpdateAvailableQuantity(Entity.Product product, int quantity);
    }
}

