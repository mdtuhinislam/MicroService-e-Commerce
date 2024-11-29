using AutoMapper;
using Basket.API.Models;
using EventBus.Message.Events;

namespace Basket.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BusketCheckOutEvent, BasketCheckout>()
                .ReverseMap();
        }
    }
}
