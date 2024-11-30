using AutoMapper;
using Basket.API.GrpcServices;
using Basket.API.Models;
using Basket.API.Repositories;
using CoreApiResponse;
using MassTransit;
using MassTransit.Testing;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BasketController : BaseController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly DiscountGrpcService _discountGrpcService;
        private readonly IPublishEndpoint _publishEndpoint;

        IMapper _mapper;
        public BasketController(IBasketRepository basketRepository, DiscountGrpcService discountGrpcService, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _basketRepository = basketRepository;
            _discountGrpcService = discountGrpcService;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBasket(string userName)
        {
            try
            {
                var basket = await _basketRepository.GetBasket(userName);
                return CustomResult("Basket data load successfully.", basket ?? new ShoppingCart(userName));
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateBasket([FromBody] ShoppingCart basket)
        {
            try
            {   //TODO: Communicate discount.grpc
                //calculate latest price
                //Create discount grpc service
                foreach (var item in basket.Items)
                {
                    var coupon = await _discountGrpcService.GetDiscount(item.ProductId);
                    item.Price -= coupon.Amount;
                }
                return CustomResult("Basket modified done.", await _basketRepository.UpdateBasket(basket));
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpDelete]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBasket(string userName)
        {
            try
            {
                await _basketRepository.DeleteBasket(userName);
                return CustomResult("Basket has been deleted.");
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]

        public async Task<IActionResult> Checkout([FromBody] BasketCheckout checkout)
        {
            var basket = await _basketRepository.GetBasket(checkout.UserName);
            if (basket == null)
            {
                return CustomResult("Basket is empty.", HttpStatusCode.BadRequest);
            }

            //Send checkout event to RabbitMQ
            //var eventMessage = _mapper.Map<BasketCheckoutEvent>(checkout);
            var eventMessage = new BasketCheckoutEvent
            {
                UserName = checkout.UserName,
                FirstName = checkout.FirstName,
                LastName = checkout.LastName,
                Address = checkout.Address,
                CardName = checkout.CardName,
                CardNumber = checkout.CardNumber,
                City = checkout.City,
                CVV = checkout.CVV,
                EmailAddress = checkout.EmailAddress,
                Expiraton = checkout.Expiraton,
                PaymentMethod = checkout.PaymentMethod,
                PhoneNumber = checkout.PhoneNumber,
                State = checkout.State,
                TotalPrice = checkout.TotalPrice,
                ZipCode = checkout.ZipCode
                
            };
            eventMessage.TotalPrice = basket.TotalPrice;
            await _publishEndpoint.Publish(eventMessage);

            //Remove basket
            await _basketRepository.DeleteBasket(basket.UserName);
            return CustomResult("Order has been placed.");
        }

    }
}