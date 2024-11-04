using Basket.API.Models;

namespace Basket.API.Repositories
{
    public interface IBasketRepository
    {
        Task<ShopingCart> GetBasket(string userName);
        Task<ShopingCart> UpdateBasket(ShopingCart basket);
        Task DeleteBusket(string userName);
    }
}
