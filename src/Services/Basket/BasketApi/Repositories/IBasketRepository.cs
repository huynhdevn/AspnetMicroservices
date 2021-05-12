using System.Threading.Tasks;
using BasketApi.Entities;

namespace BasketApi.Repositories
{
    public interface IBasketRepository
    {
        Task<Basket> GetBasket(string userName);
        Task<Basket> UpdateBasket(Basket basket);
        Task DeleteBasket(string userName);

    }
}