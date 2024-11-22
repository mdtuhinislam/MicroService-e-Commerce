using AutoMapper;
using Ordering.Application.Features.Orders.Commands.CreateOrder;
using Ordering.Application.Features.Orders.Queries.GetOrders;
using Ordering.Domain.Models;

namespace Ordering.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            #region Order

            CreateMap<Order, OrderVM>().ReverseMap();
            CreateMap<Order, CreateOrderCommand>().ReverseMap();

            #endregion
        }
    }
}
