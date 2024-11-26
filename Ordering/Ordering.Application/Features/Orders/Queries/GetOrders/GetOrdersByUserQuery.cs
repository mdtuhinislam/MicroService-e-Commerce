using MediatR;

namespace Ordering.Application.Features.Orders.Queries.GetOrders
{
    public class GetOrdersByUserQuery : IRequest<List<OrderVM>>
    {
        public string UserName { get; set; }

        public GetOrdersByUserQuery(string userName)
        {
            UserName = userName;
        }
    }
}
