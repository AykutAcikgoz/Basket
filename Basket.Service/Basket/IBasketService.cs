using System;
using Basket.Service.Payload;

namespace Basket.Service.Basket
{
    public interface IBasketService
    {
        Task<ResponsePayload> AddtoBasket(Entity.Product product, int quantity);
        Task<ResponsePayload> RemoveFromBasket(Entity.Product product);
        Task<ResponsePayload> UpdateBasketItemQuantity(Entity.Product product, int quantity);
        Task<ResponsePayload> GetBasket();
        ResponsePayload RemoveBasket();
        Task<ResponsePayload> PlaceOrder();
    }
}

