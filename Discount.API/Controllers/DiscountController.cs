using Discount.API.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Discount.API.Models;

namespace Discount.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountDiscountController : ControllerBase
    {
        private readonly ICouponRepository _couponRepository;

        public DiscountDiscountController(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        [ResponseCache(Duration = 300)]
        public async Task<IActionResult> GetDiscount(string id) 
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id))
                    return BadRequest();
                var discount = await _couponRepository.GetDiscountAsync(id);

                if (discount is null)
                    return BadRequest();
                return Ok(discount);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet("getAll")]
        [ProducesResponseType(typeof(IEnumerable<Coupon>), (int)HttpStatusCode.OK)]
        [ResponseCache(Duration = 30)]
        public IActionResult GetAll()
        {
            try
            {
                var items = _couponRepository.GetAllDiscountAsync();

                if (items is not null)
                    return Ok(items);
                return BadRequest("No Item");
            }
            catch (Exception ex)
            {

                return BadRequest("No Item");
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Coupon coupon)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Ok(await _couponRepository.CreateDiscountAsync(coupon));

                }
                return BadRequest("Some reqired fiels are missing!");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] Coupon coupon)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Ok(await _couponRepository.UpdateDiscountAsync(coupon));

                }
                return BadRequest("Some reqired fiels are missing!");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return BadRequest("Data not Found!");
                }
                var items = _couponRepository.GetDiscountAsync(id);
                if (items is not null)
                    return await _couponRepository.DeleteDiscountAsync(id) ? Ok("Data Deleted!") : BadRequest("Something went wrong");
                      
                return BadRequest("No Item");
            }
            catch (Exception ex)
            {

                return BadRequest("No Item");
            }


        }
    }
}
