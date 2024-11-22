using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Ordering.Application.Contract.Persistences;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;

        public UpdateOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        async Task<bool> IRequestHandler<UpdateOrderCommand, bool>.Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var isSuccess = await _orderRepository.UpdateAsync(request);
            if (isSuccess)
                return isSuccess;
            else return true;
        }
    }
}
