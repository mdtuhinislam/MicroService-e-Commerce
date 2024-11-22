using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Ordering.Application.Contract.Persistences;

namespace Ordering.Application.Features.Orders.Queries.GetOrders
{
    public class GetOrdersByUserHandler : IRequestHandler<GetOrdersByUserQuery, IList<OrderVM>>
    {
        private readonly IOrderRepository _repository;
        private readonly IMapper _mapper;

        public GetOrdersByUserHandler(IOrderRepository orderRepository,IMapper mapper)
        {
            _mapper = mapper;
            _repository = orderRepository;
        }

        

        public async Task<IList<OrderVM>> Handle(GetOrdersByUserQuery request, CancellationToken cancellationToken)
        {
            var orders = await _repository.GetOrdersByUserNameAsync(request.UserName);
            return _mapper.Map<List<OrderVM>>(orders);

        }
    }
}
