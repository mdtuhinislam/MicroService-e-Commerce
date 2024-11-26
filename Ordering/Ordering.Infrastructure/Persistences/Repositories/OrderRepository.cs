using System.Text;
using EF.Core.Repository.Repository;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contract.Persistences;
using Ordering.Domain.Models;

namespace Ordering.Infrastructure.Persistences.Repositories
{
    public class OrderRepository : CommonRepository<Order>, IOrderRepository
    {
        private readonly OrderDbContext _context;
        public OrderRepository(OrderDbContext orderDbContext, OrderDbContext context) : base(orderDbContext)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserNameAsync(string userName)
        {
            try
            {
                if (string.IsNullOrEmpty(userName))
                    return Enumerable.Empty<Order>();

                // Normalize the input userName
                string normalizedUserName = NormalizeString(userName);

                // Perform the query
                return await _context.Orders
                    .Where(x => x.UserName.ToLower() == normalizedUserName)
                    .ToListAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }
        public static string NormalizeString(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            return input.Trim()
                        .ToLowerInvariant()
                        .Normalize(NormalizationForm.FormC); // Normalizes Unicode characters
        }

    }
}
