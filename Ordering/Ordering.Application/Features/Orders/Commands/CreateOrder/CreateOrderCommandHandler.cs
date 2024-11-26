using AutoMapper;
using MediatR;
using Ordering.Application.Contract.Infrastructures;
using Ordering.Application.Contract.Persistences;
using Ordering.Application.Models;
using Ordering.Domain.Models;

namespace Ordering.Application.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        public CreateOrderCommandHandler(IOrderRepository orderRepository,
            IMapper mapper,
            IEmailService emailService)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _emailService = emailService;
        }

        public async Task<bool> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<Order>(request);
            order.CreatedDate = DateTime.Now;
            order.CreatedBy = "1";

            var isSuccess = await _orderRepository.AddAsync(order);
            if (isSuccess)
            {
                await _emailService.SendEmailAsync(new EmailMassage
                {
                    To = request.EmailAddress,
                    Subject = $"Order {order.Id} has been successfully placed.",
                    Body = $"Message body from Order"
                });
                return isSuccess;
            }
                
            else return false;
        }
    }
}
