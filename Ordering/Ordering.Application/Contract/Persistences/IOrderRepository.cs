using EF.Core.Repository.Interface.Repository;
using Ordering.Domain.Models;

namespace Ordering.Application.Contract.Persistences
{
    public interface IOrderRepository : ICommonRepository<Order>
    {
       Task<IEnumerable<Order>> GetOrdersByUserNameAsync(string userName);
    }
}
