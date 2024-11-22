using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Ordering.Application.Contract.Persistences;

namespace Ordering.Application.Features.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, bool>
    {
        private readonly IOrderRepository _repository;
        private readonly IMapper _mapper;
        public DeleteOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _repository = orderRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _repository.GetFirstOrDefaultAsync(x => x.Id == request.Id);
            if (order == null)
            {
                return false;
            }
            return await _repository.DeleteAsync(order);
        }
    }
}
