using AutoMapper;
using Basket.API.Entities;
using Basket.API.GrpcServices;
using Basket.API.Repositories;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepo;
        private readonly DiscountGrpcService _discountGrpcService;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEnpoint;

        public BasketController(IBasketRepository basketRepo, DiscountGrpcService discountGrpcService, 
            IMapper mapper, IPublishEndpoint publishEnpoint)
        {
            _basketRepo = basketRepo ?? throw new ArgumentNullException(nameof(basketRepo));
            _discountGrpcService = discountGrpcService ?? throw new ArgumentNullException(nameof(discountGrpcService));
            _mapper = mapper;
            _publishEnpoint = publishEnpoint;
        }

        [HttpGet("{username}")]
        public async Task<ShoppingCart> GetBasket(string username)
        {
            var basket = await _basketRepo.GetBasket(username);

            return basket ?? new ShoppingCart(username);
        }

        [HttpPost]
        public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
        {
            // Consumming Grpc
            foreach(var item in basket.Items)
            {
                var coupon = await _discountGrpcService.GetDiscount(item.ProductName);
                item.Price -= coupon.Amount;
            }    

            return await _basketRepo.UpdateBasket(basket);
        }

        [HttpDelete("{username}")]
        public async Task<IActionResult> DeleteBasket(string username)
        {
            await _basketRepo.DeleteBasket(username);
                
            return Ok();
        }

        /* get existing basket with total price
         * create basketCheckoutEvent -- Set TotalPrice on basketCheckout eventMessage
         * send checkout event to rabitmq
         * remove the basket */
        [HttpPost("[action]")]
        public async Task<IActionResult> Checkout(BasketCheckout basketCheckout)
        {
            var basket = await _basketRepo.GetBasket(basketCheckout.UserName);

            if (basket == null) return BadRequest();

            var eventMessage = _mapper.Map<BasketCheckoutEvent>(basketCheckout);
            eventMessage.TotalPrice = basket.TotalPrice;
            await _publishEnpoint.Publish(eventMessage);

            await _basketRepo.DeleteBasket(basketCheckout.UserName);

            return Accepted();
        }
    }
}
