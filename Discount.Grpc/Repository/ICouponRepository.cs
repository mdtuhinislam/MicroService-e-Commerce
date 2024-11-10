using Discount.Grpc.Models;

namespace Discount.Grpc.Repository
{
    public interface ICouponRepository
    {
        Task<Coupon> GetDiscountAsync(string productId);
        Task<IEnumerable<Coupon>> GetAllDiscountAsync();
        Task<bool> CreateDiscountAsync(Coupon coupon);
        Task<bool> UpdateDiscountAsync(Coupon coupon);
        Task<bool> DeleteDiscountAsync(string productId);
    }
}
