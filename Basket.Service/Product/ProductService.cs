using System;
using Basket.Data.Product;
using Basket.Service.Payload;

namespace Basket.Service.Product
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public ResponsePayload GetAllProducts()
        {
            var products = _productRepository.Get();
            return new SuccessPayload<IEnumerable<Entity.Product>>() { Data = products.ToList() };
        }

        public async Task<ResponsePayload> GetProductByIdAsync(string id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) return new ErrorPayload() { ErrorMessage = "Product not found!" };

            return new SuccessPayload<Entity.Product>() { Data = product };
        }

        public async Task<bool> UpdateAvailableQuantity(Entity.Product product, int quantity)
        {
            try{
                product.AvailableQuantity = quantity;
                await _productRepository.UpdateAsync(product.Id, product);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
            
        }
    }
}

