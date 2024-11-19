using Discount_gRPC.Models;

namespace Discount_gRPC.Repository
{
    public interface ICouponRepository
    {
        Task<Coupon> GetDiscountAsync(string productId);
        Task<bool> CreateDiscount(Coupon coupon);
        Task<bool> UpdateDiscount(Coupon coupon);
        Task<bool> DeleteDiscount(string productId);
    }
}
