using System.Net;
using AutoMapper;
using CoreApiResponse;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Features.Orders.Commands.CreateOrder;
using Ordering.Application.Features.Orders.Commands.DeleteOrder;
using Ordering.Application.Features.Orders.Commands.UpdateOrder;
using Ordering.Application.Features.Orders.Queries.GetOrders;

namespace Ordering.API.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class OrderController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public OrderController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OrderVM>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetOrderByUserName(string userName)
        {
            try
            {
                var orders = await _mediator.Send(new GetOrdersByUserQuery(userName));
                if (orders == null || !orders.Any()) 
                {
                    return CustomResult("Order not found!", HttpStatusCode.NotFound);
                }
                return CustomResult("Orders found successfully!", orders);
            }
            catch (Exception ex)
            {

                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateOrder(CreateOrderCommand createOrder)
        {
            try
            {
                var isCreated = await _mediator.Send(createOrder);
                if (!isCreated)
                {
                    return CustomResult("Order has not created");
                }
                return CustomResult("Orders created successfully!", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateOrder(UpdateOrderCommand updateOrder)
        {
            try
            {
                var isModified = await _mediator.Send(updateOrder);
                if (!isModified)
                {
                    return CustomResult("Order has not updated!");
                }
                return CustomResult("Orders updated successfully!", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpDelete]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            try
            {
                var isModified = await _mediator.Send(new DeleteOrderCommand { Id = orderId});
                if (!isModified)
                {
                    return CustomResult("Order has not deleted!");
                }
                return CustomResult("Orders deleted successfully!", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }
    }
}
