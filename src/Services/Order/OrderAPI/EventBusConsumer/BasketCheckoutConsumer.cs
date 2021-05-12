using System.Threading.Tasks;
using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using OrderApplication.Features.Orders.Commands.CheckoutOrder;

namespace Services.Order.OrderAPI.EventBusConsumer
{
    public class BasketCheckoutConsumer : IConsumer<BasketCheckoutEvent>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly ILogger<BasketCheckoutConsumer> _logger;

        public BasketCheckoutConsumer(IMapper mapper, IMediator mediator, ILogger<BasketCheckoutConsumer> logger)
        {
            _mediator = mediator;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            var command = _mapper.Map<CheckoutOrderCommand>(context.Message);
            var result = await _mediator.Send(command);

            _logger.LogInformation("BasketCheckoutEvent consumed successfully. Created Order Id: {newOrderId}", result);
        }
    }
}