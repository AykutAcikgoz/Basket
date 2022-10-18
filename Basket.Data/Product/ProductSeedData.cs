using System;

namespace Basket.Data.Product
{
    public class ProductSeedData
    {
        private IProductRepository _repository;
        public ProductSeedData(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task SeedDataAsync()
        {
            if (!await _repository.AnyAsync())
            {
                await _repository.AddAsync(
                    new Entity.Product
                    {
                        Name = "Cicek#1",
                        Category = "Category#1",
                        Description = "Description#1",
                        AvailableQuantity = 10,
                        Price = 100
                    });
                await _repository.AddAsync(
                    new Entity.Product
                    {
                        Name = "Cicek#2",
                        Category = "Category#2",
                        Description = "Description#2",
                        AvailableQuantity = 5,
                        Price = 29
                    });
                await _repository.AddAsync(
                    new Entity.Product
                    {
                        Name = "Cicek#3",
                        Category = "Category#1",
                        Description = "Description#3",
                        AvailableQuantity = 2,
                        Price = 55
                    });
            }
        }
    }
}

