using AutoMapper;
using EventBus.Messages.Events;
using OrderApplication.Features.Orders.Commands.CheckoutOrder;

namespace Services.Order.OrderAPI.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BasketCheckoutEvent, CheckoutOrderCommand>().ReverseMap();
        }

    }
}