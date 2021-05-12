using System.Text.Json;
using System.Threading.Tasks;
using BasketApi.Entities;
using Microsoft.Extensions.Caching.Distributed;

namespace BasketApi.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCache;

        public BasketRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache;
        }

        public async Task<Basket> GetBasket(string userName)
        {
            var basketJson = await _redisCache.GetStringAsync(userName);

            return basketJson == null ? null : JsonSerializer.Deserialize<Basket>(basketJson);
        }

        public async Task<Basket> UpdateBasket(Basket basket)
        {
            await _redisCache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket));

            return await GetBasket(basket.UserName);
        }

        public async Task DeleteBasket(string userName)
        {
            await _redisCache.RemoveAsync(userName);
        }
    }
}