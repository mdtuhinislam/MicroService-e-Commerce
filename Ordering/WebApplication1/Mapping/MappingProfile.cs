using AutoMapper;
using EventBus.Message.Events;
using Ordering.API.EventBusConsumers;
using Ordering.Application.Features.Orders.Commands.CreateOrder;

namespace Ordering.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BusketCheckOutEvent, CreateOrderCommand>().
                ReverseMap();
        }
    }
}
