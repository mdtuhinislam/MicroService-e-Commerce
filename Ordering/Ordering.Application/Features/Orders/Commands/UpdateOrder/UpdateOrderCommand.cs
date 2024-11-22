using MediatR;
using Ordering.Domain.Models;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommand : Order, IRequest<bool>
    {
        
    }
}
