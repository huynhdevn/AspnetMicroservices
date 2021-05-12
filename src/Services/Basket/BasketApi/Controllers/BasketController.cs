using System.Threading.Tasks;
using AutoMapper;
using BasketApi.Entities;
using BasketApi.Models;
using BasketApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using EventBus.Messages.Events;
using System;
using System.Linq;
using MassTransit;

namespace BasketApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public BasketController(IBasketRepository basketRepo, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
            _basketRepo = basketRepo;
            _mapper = mapper;
        }

        [HttpGet("{userName}")]
        public async Task<Basket> Get(string userName)
        {
            var basket = await _basketRepo.GetBasket(userName);

            return basket ?? new Basket(userName);
        }

        [HttpPost]
        public async Task<ActionResult<Basket>> Create(Basket basket)
        {
            return await _basketRepo.UpdateBasket(basket);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Checkout(BasketCheckout basketCheckout)
        {
            var basket = await _basketRepo.GetBasket(basketCheckout.UserName);

            var eventMessage = _mapper.Map<BasketCheckoutEvent>(basketCheckout);
            eventMessage.TotalPrice = basket.Items.Sum(item => item.Quantity * item.Price);

            await _publishEndpoint.Publish(eventMessage);

            await _basketRepo.DeleteBasket(basketCheckout.UserName);

            return Accepted();
        }

    }
}