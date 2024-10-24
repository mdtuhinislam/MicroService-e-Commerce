using System.Net;
using Catalog.API.Interfaces.Manager;
using Catalog.API.Models;
using CoreApiResponse;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : BaseController
    {
        private readonly IProductManager _productManager;
        public CatalogController(IProductManager productManager)
        {
            _productManager = productManager;
        }

        [HttpGet("getAll")]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        [ResponseCache(Duration =300)]
        public  IActionResult GetAll()
        {
            try
            {
                var items = _productManager.GetAll();
                if (items is not null && items.Any())
                    return CustomResult("Data Found", items, HttpStatusCode.OK);
                return BadRequest("No Item");
            }
            catch (Exception ex)
            {

                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
            

        }
        [HttpPost("createOrUpdate")]
        public IActionResult CreateOrUpdate(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (string.IsNullOrEmpty(product.Id))
                    {
                        product.Id = Guid.NewGuid().ToString();
                        var isAdded = _productManager.Add(product);
                        if (isAdded)
                            return CustomResult("Data added successfully!", HttpStatusCode.OK);
                    }
                    else
                    {
                        var item = _productManager.GetFirstOrDefault(x => x.Id == product.Id);
                        if (item is not null)
                        {
                            item.Name = product.Name;
                            item.Description = product.Description;
                            item.Category = product.Category;
                            item.Summary = product.Summary;
                            item.ImageUrl = product.ImageUrl;
                            item.Price = product.Price;

                            var isUpdated = _productManager.Update(product.Id, item);
                            if (isUpdated)
                                return CustomResult("Data updated successfully!", HttpStatusCode.OK);
                        }
                    }

                }
                return CustomResult("Some reqired fiels are missing!", HttpStatusCode.BadGateway);
            }
            catch (Exception ex)
            {

                return CustomResult(ex.Message, HttpStatusCode.BadGateway);
            }
            
        }
        [HttpDelete]
        public IActionResult Delete(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return CustomResult("Data not Found!", HttpStatusCode.NotFound);
                }
                var items = _productManager.GetById(id);
                if (items is not null)
                    return _productManager.Delete(id) ? CustomResult("Data Deleted!", HttpStatusCode.OK) :
                       CustomResult("Something went wrong!", HttpStatusCode.NotModified);
                return BadRequest("No Item");
            }
            catch (Exception ex)
            {

                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }


        }
    }
}
