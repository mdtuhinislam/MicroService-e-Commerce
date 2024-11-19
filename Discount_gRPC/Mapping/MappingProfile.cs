using AutoMapper;
using Discount_gRPC.Models;
using Discount_gRPC.Protos;

namespace Discount.Grpc.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Coupon, CouponRequest>().ReverseMap();
        }
    }
}
