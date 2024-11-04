using Basket.API.Models;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCache;

        public BasketRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache;
        }
        
        public async Task<ShopingCart> GetBasket(string userName)
        {
            string basket = await _redisCache.GetStringAsync(userName);

            if (string.IsNullOrEmpty(basket))
                throw null;
            return JsonConvert.DeserializeObject<ShopingCart>(basket);
        }

        public async Task<ShopingCart> UpdateBasket(ShopingCart basket)
        {
           await _redisCache.SetStringAsync(basket.UserName, JsonConvert.SerializeObject (basket.Items));
            return await GetBasket(basket.UserName);
        }
        public async Task DeleteBusket(string userName)
        {
            await _redisCache.RemoveAsync(userName);
        }
    }
}
