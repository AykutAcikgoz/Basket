using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Basket.Data.Product;
using Basket.Entity;
using Basket.Service.Basket;
using Basket.Service.Payload;
using Basket.Store;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpGet(Name = "Get")]
        public async Task<ResponsePayload> Get()
        {
            return await _basketService.GetBasket();
        }

        [HttpPost(Name = "Add")]
        public async Task<ResponsePayload> Add(Entity.Product product, int quantity)
        {
            if (quantity == 0) return new ErrorPayload() { ErrorMessage = "Invalid quantity value" };
            return await _basketService.AddtoBasket(product, quantity);
        }

        [HttpPost("UpdateBasketItem")]
        public async Task<ResponsePayload> Update(Entity.Product product, int quantity)
        {
            if (quantity == 0) return new ErrorPayload() { ErrorMessage = "Invalid quantity value" };
            return await _basketService.UpdateBasketItemQuantity(product, quantity);
        }

        [HttpDelete(Name = "Delete")]
        public async Task<ResponsePayload> Delete(Entity.Product product)
        {
            return await _basketService.RemoveFromBasket(product);
        }

        [HttpDelete("DeleteBasket")]
        public ResponsePayload DeleteBasket()
        {
            return _basketService.RemoveBasket();
        }

        [HttpPost("PlaceOrder")]
        public async Task<ResponsePayload> PlaceOrder()
        {
            return await _basketService.PlaceOrder();
        }

    }
}

