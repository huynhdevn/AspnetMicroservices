using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderApplication.Features.Orders.Commands.CheckoutOrder;
using OrderApplication.Features.Orders.Queries.GetOrderList;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{userName}")]
        public async Task<IEnumerable<OrderVm>> GetOrderBy(string userName)
        {
            var request = new GetOrderListQuery(userName);
            var orders = await _mediator.Send(request);
            
            return orders;
        }

        [HttpPost]
        public async Task<int> Checkout(CheckoutOrderCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
