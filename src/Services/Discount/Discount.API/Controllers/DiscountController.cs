using Discount.API.Entities;
using Discount.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Discount.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DiscountController : Controller
    {
        private readonly IDiscountRepository _repository;

        public DiscountController(IDiscountRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet("{productName}")]
        public async Task<ActionResult<Coupon>> GetDiscount(string productName)
        {
            return await _repository.GetDiscount(productName);
        }

        [HttpPost]
        public async Task<ActionResult<Coupon>> CreateDiscount(Coupon coupon)
        {
            await _repository.CreateDiscount(coupon);

            return CreatedAtAction("GetDiscount", new { productName = coupon.ProductName }, coupon);
        }

        [HttpPut]
        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            return await _repository.UpdateDiscount(coupon);
        }

        [HttpDelete("{productName}")]
        public async Task<bool> DeleteDiscount(string productName)
        {
            return await _repository.DeleteDiscount(productName);
        }
    }
}
