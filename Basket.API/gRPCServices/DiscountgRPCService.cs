using Discount.Grpc.Protos;

namespace Basket.API.gRPCServices
{
    public class DiscountgRPCService
    {
        public readonly DiscountProtoService.DiscountProtoServiceClient _discountService;

        public DiscountgRPCService(DiscountProtoService.DiscountProtoServiceClient discountService)
        {
            _discountService = discountService;
        }

        public async Task<CouponRequest> GetDiscount(string productId)
        {
            return await _discountService.GetDiscountAsync(new GetDiscountRequest { ProductId = productId});

        }
    }
}
