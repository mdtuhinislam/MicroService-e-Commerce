using System.Net;
using Basket.API.Models;
using Basket.API.Repositories;
using CoreApiResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class BasketController : BaseController
    {
        private readonly IBasketRepository _basketRepository;
        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ShopingCart), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBasket(string userName)
        {
            try
            {
                var basket = await _basketRepository.GetBasket(userName);

                if (basket is null)
                    return CustomResult(new ShopingCart(), HttpStatusCode.NotFound);

                return CustomResult(basket, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(ShopingCart), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateBasket([FromBody] ShopingCart shopingCart)
        {
            try
            {
                if (shopingCart is null || !ModelState.IsValid)
                    return CustomResult(HttpStatusCode.BadRequest);

                var updatedBasket = await _basketRepository.UpdateBasket(shopingCart);
                if(updatedBasket is null)
                    return CustomResult("Something Went Wrong, Update unsuccessful!", HttpStatusCode.BadRequest);

                return CustomResult(updatedBasket, HttpStatusCode.OK);           
            }
            catch (Exception ex)
            {

                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpDelete]
        [ProducesResponseType(typeof(ShopingCart), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete([FromBody] string userName)
        {
            try
            {
                if (string.IsNullOrEmpty(userName))
                    return CustomResult(HttpStatusCode.BadRequest);

                await _basketRepository.DeleteBusket(userName);

                return CustomResult("Basket deleted!", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }
    }
}
