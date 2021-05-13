using Microsoft.AspNetCore.Mvc;
using Shopping.Aggregator.Models;
using Shopping.Aggregator.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopping.Aggregator.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ShoppingController : ControllerBase
    {
        private readonly ICatalogService _catalogService;
        private readonly IBasketService _basketService;
        private readonly IOrderService _orderService;

        public ShoppingController(ICatalogService catalogService, IBasketService basketService, IOrderService orderService)
        {
            _catalogService = catalogService ?? throw new ArgumentNullException(nameof(catalogService));
            _basketService = basketService ?? throw new ArgumentNullException(nameof(basketService));
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
        }

        /* Get basket with username
         * iterate basket items and consume products with basket item productId memeber
         * map product related members into basketitem dto with exended columns
         * consume ordering microservices in order to retreive order list
         * return root ShoppingModel dto class which including all responses
         */
        [HttpGet("{userName}")]
        public async Task<ActionResult<ShoppingModel>> GetShopping(string userName)
        {
            var basket = await _basketService.GetBasket(userName);

            basket.Items.ForEach(async item =>
            {
                var product = await _catalogService.GeCatalog(item.ProductId);
                item.Category = product.Category;
                item.Summary = product.Summary;
                item.Description = product.Description;
                item.ImageFile = product.ImageFile;
            });

            var orders = await _orderService.GetOrdersByUserName(userName);

            return new ShoppingModel
            {
                UserName = userName,
                BasketWithProducts = basket,
                Orders = orders
            };
        }
    }
}
