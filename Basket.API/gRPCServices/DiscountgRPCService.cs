using Discount_gRPC.Protos;

namespace Basket.API.GrpcServices
{
    public class DiscountGrpcService
    {
        public readonly DiscountProtoService.DiscountProtoServiceClient _discountService;
        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountService)
        {
            _discountService = discountService;
        }

        public async Task<CouponRequest> GetDiscount(string productId)
        {
            try
            {
                var getDisCountData = new GetDiscountRequest() { ProductId = productId };
                var result =  await _discountService.GetDiscountAsync(getDisCountData);
                return result;
                
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }
    }
}