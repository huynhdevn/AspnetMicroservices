using AutoMapper;
using OrderApplication.Features.Orders.Commands.CheckoutOrder;
using OrderApplication.Features.Orders.Queries.GetOrderList;
using OrderDomain.Entities;

namespace OrderApplication.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, OrderVm>().ReverseMap();
            CreateMap<CheckoutOrderCommand, Order>().ReverseMap();
        }
    }
}