using Basket.API.Entities;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _repository;

        public BasketController(IBasketRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet("{username}")]
        public async Task<ShoppingCart> GetBasket(string username)
        {
            var basket = await _repository.GetBasket(username);

            return basket ?? new ShoppingCart(username);
        }

        [HttpPost]
        public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
        {
            return await _repository.UpdateBasket(basket);
        }

        [HttpDelete("{username}")]
        public async Task<IActionResult> DeleteBasket(string username)
        {
            await _repository.DeleteBasket(username);
                
            return Ok();
        }
    }
}
