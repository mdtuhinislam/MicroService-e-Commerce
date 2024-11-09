using Dapper;
using Discount.API.Models;
using Npgsql;

namespace Discount.API.Repository
{
    public class CouponRepository : ICouponRepository
    {
        private readonly IConfiguration _configuration;

        public CouponRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> CreateDiscountAsync(Coupon coupon)
        {
            try
            {
                if (coupon is null)
                    return false;

                var connection = new NpgsqlConnection(_configuration.GetConnectionString("DiscountDB"));

                var affected = await connection.ExecuteAsync("INSERT INTO Coupon(ProductId, ProductName, Description, Amount)VALUES(@ProductId, @ProductName, @Description, @Amount)", new
                {
                    ProductId = coupon.ProductId,
                    ProductName = coupon.ProductName,
                    Description = coupon.Description,
                    Amount = coupon.Amount
                });

                if (affected > 0)
                    return true;
                return false;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<bool> DeleteDiscountAsync(string productId)
        {
            try
            {
                if (string.IsNullOrEmpty(productId))
                    return false;

                var connection = new NpgsqlConnection(_configuration.GetConnectionString("DiscountDB"));

                var affected = await connection.ExecuteAsync("DELETE FROM Coupon WHERE ProductId = @ProductId", new
                {
                    ProductId = productId
                });

                if (affected > 0)
                    return true;
                return false;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Coupon>> GetAllDiscountAsync()
        {
            try
            {
                // Using the 'using' statement to ensure proper disposal of the connection
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DiscountDB")))
                {
                    await connection.OpenAsync();

                    // Use QueryAsync instead of QueryMultipleAsync when expecting a single result
                    var coupons = await connection.QueryAsync<Coupon>("SELECT * FROM Coupon");

                    // If no results, return an empty collection
                    return coupons ?? Enumerable.Empty<Coupon>();
                }
            }
            catch (Exception ex)
            {
                // Log or rethrow the exception with more context
                throw new ApplicationException("An error occurred while retrieving coupons", ex);
            }
        }


        public async Task<Coupon> GetDiscountAsync(string productId)
        {
            try
            {
                if (string.IsNullOrEmpty(productId))
                    return null;

                var connection = new NpgsqlConnection(_configuration.GetConnectionString("DiscountDB"));
                var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>("SELECT * FROM Coupon WHERE ProductId = @ProductId", new { ProductId = productId });
                if (coupon is null)
                {
                    return new Coupon();
                }
                connection.Close();
                return coupon;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<bool> UpdateDiscountAsync(Coupon coupon)
        {
            try
            {
                if (coupon is null)
                    return false;

                var connection = new NpgsqlConnection(_configuration.GetConnectionString("DiscountDB"));

                var affected = await connection.ExecuteAsync("UPDATE Coupon SET ProductName = @ProductName, Description = @Description, Amount = @Amount WHERE ProductId=@ProductId, ", new
                {
                    ProductId = coupon.ProductId,
                    ProductName = coupon.ProductName,
                    Description = coupon.Description,
                    Amount = coupon.Amount
                });

                if (affected > 0)
                    return true;
                return false;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
