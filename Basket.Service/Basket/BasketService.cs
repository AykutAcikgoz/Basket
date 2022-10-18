using System;
using Basket.Data.Basket;
using Basket.Entity.Basket;
using Basket.Service.Payload;
using Basket.Service.Product;
using Basket.Store;
using Microsoft.AspNetCore.Http;

namespace Basket.Service.Basket
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IBasketStore _basketStore;
        private readonly IProductService _productService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BasketService(IBasketRepository basketRepository, IBasketStore basketStore, IProductService productService, IHttpContextAccessor httpContextAccessor)
        {
            _basketRepository = basketRepository;
            _basketStore = basketStore;
            _productService = productService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResponsePayload> AddtoBasket(Entity.Product product, int quantity)
        {
            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId)) return UserNotFound();

            var basket = await _basketStore.GetAsync<Entity.Basket.Basket>(userId);

            if (basket == null)
            {
                basket = new Entity.Basket.Basket();
            }
            basket.ClientId = userId;

            var basketItem = new Entity.Basket.BasketItem();

            var payload = await _productService.GetProductByIdAsync(product.Id);
            
            if(payload is ErrorPayload) return new ErrorPayload() { ErrorMessage = "Product not found!" };

            var dbProduct = payload as SuccessPayload<Entity.Product>;

            if ((dbProduct as SuccessPayload<Entity.Product>).Data.AvailableQuantity < quantity) return new ErrorPayload() { ErrorMessage = "Not enough stock is available!" };

            basketItem.Quantity = basketItem.Quantity + quantity;
            basketItem.Product = dbProduct.Data;

            basket.Items.Add(basketItem);

            var updated = _basketStore.Set<Entity.Basket.Basket>(userId, basket);

            return new SuccessPayload<Entity.Basket.Basket>(){ Data = basket };


        }
        
        public async Task<ResponsePayload> RemoveFromBasket(Entity.Product product)
        {
            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId)) return UserNotFound();

            var basket = await _basketStore.GetAsync<Entity.Basket.Basket>(userId);

            if (basket == null) return new ErrorPayload() { ErrorMessage = "Basket not found!" };

            var removed = basket.Items.Remove(basket.Items.FirstOrDefault(t => t.Product.Id == product.Id));

            if (!removed) return new ErrorPayload() { ErrorMessage = "Could not remove product from basket!" };

            var updated = _basketStore.Set<Entity.Basket.Basket>(userId, basket);

            if (!updated) return new ErrorPayload() { ErrorMessage = "Item removed from basket but unable to update store!" };

            return new SuccessPayload<Entity.Basket.Basket>() { Data = basket };

        }

        public async Task<ResponsePayload> UpdateBasketItemQuantity(Entity.Product product, int quantity)
        {
            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId)) return UserNotFound();

            var basket = await _basketStore.GetAsync<Entity.Basket.Basket>(userId);

            if (basket == null) basket = new Entity.Basket.Basket();

            basket.ClientId = userId;

            var basketItem = basket.Items.FirstOrDefault(t => t.Product?.Id == product.Id);

            if (basketItem == null) return new ErrorPayload() { ErrorMessage = "Product not found in basket!" };

            var dbProduct = await _productService.GetProductByIdAsync(product.Id);

            if (dbProduct is ErrorPayload) return new ErrorPayload() { ErrorMessage = "Product not found in store!" };

            if ((dbProduct as SuccessPayload<Entity.Product>).Data.AvailableQuantity < basketItem.Quantity + quantity) return new ErrorPayload() { ErrorMessage = "Not enough stock is available!" };

            basketItem.Quantity = quantity;

            var updated = _basketStore.Set<Entity.Basket.Basket>(userId, basket);

            if(!updated) return new ErrorPayload() { ErrorMessage = "Unable to update quantity for the product" };

            return new SuccessPayload<Entity.Basket.Basket>() { Data = basket };
        } 

        public ResponsePayload RemoveBasket()
        {
            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId)) return UserNotFound();

            var removed = _basketStore.Remove(userId);
            if(!removed) return new ErrorPayload() { ErrorMessage = "Unable to remove basket from store!" };

            return new SuccessPayload<object>() { Data = null };

        }

        private string GetUserId()
        {
            return _httpContextAccessor.HttpContext.Request.Cookies["SESSION_ID"];
        }

        private ErrorPayload UserNotFound()
        {
            return new ErrorPayload() { ErrorMessage = "User not found!" };
        }

        public async Task<ResponsePayload> GetBasket()
        {
            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId)) return UserNotFound();

            var basket = await _basketStore.GetAsync<Entity.Basket.Basket>(userId);

            return new SuccessPayload<Entity.Basket.Basket>() { Data = basket };
        }

        public async Task<ResponsePayload> PlaceOrder()
        {
            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId)) return UserNotFound();

            var basket = await _basketStore.GetAsync<Entity.Basket.Basket>(userId);

            if (basket == null) return new ErrorPayload() { ErrorMessage = "Basket not found!" };

            if (basket.Items.Count == 0) return new ErrorPayload() { ErrorMessage = "Basket is empty!" };


            try
            {
                await _basketRepository.AddAsync(basket);
                basket.Items.ForEach((item) =>
                {
                    _productService.UpdateAvailableQuantity(item.Product, item.Product.AvailableQuantity -= item.Quantity).Wait();
                });
                RemoveBasket();
            }
            catch(Exception ex)
            {
                return new ErrorPayload() { Exception = ex, ErrorMessage = "An error occured while trying to place order!" };
            }

            return new SuccessPayload<object>() { Data = null };
        }
    }
}

