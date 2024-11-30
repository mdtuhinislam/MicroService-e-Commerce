using AutoMapper;
using EventBus.Message.Events;
using MassTransit;
using MediatR;
using Ordering.Application.Features.Orders.Commands.CreateOrder;

namespace Ordering.API.EventBusConsumers
{
    public class BasketCheckOutConsumer : IConsumer<BusketCheckOutEvent>
    {
        private readonly IMediator _mediator;
        ILogger<BasketCheckOutConsumer> _logger;
        IMapper _mapper;
        public BasketCheckOutConsumer(IMediator mediator, ILogger<BasketCheckOutConsumer> logger, IMapper mapper)
        {
            _mediator = mediator;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task Consume(ConsumeContext<BusketCheckOutEvent> context)
        {
            var orderData = _mapper.Map<CreateOrderCommand>(context.Message);
            if(await _mediator.Send(orderData))
            {
                _logger.LogInformation("Basket Event has been consumed for user{0}", orderData.UserName);
            }
            else _logger.LogInformation("Basket Event has been faildfor user {0}", orderData.UserName);
        }
    }
}
