using System;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using Basket.Data.Product;

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {

        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet(Name = "GetProducts")]
        public async Task<IQueryable<Entity.Product>> Get()
        {

            return _productRepository.Get();
        }
    }
}

