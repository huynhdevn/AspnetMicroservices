using AutoMapper;
using BasketApi.Models;
using EventBus.Messages.Events;

namespace Services.Basket.BasketApi.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BasketCheckout, BasketCheckoutEvent>().ReverseMap();
        }
    }
}