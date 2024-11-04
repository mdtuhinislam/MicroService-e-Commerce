using Catalog.API.Managers;
using Catalog.API.Models;

namespace Catalog.API.Context
{
    public class CatalogDbContextSeed
    {
        private static readonly ProductManager _productManager = new ProductManager();

        public static  void Seed()
        {
            var product = _productManager.GetFirstOrDefault(x => true);
            if(product is null)
            {
                _productManager.Add(getListOfProducts());
            }
        }

        private static Product getListOfProducts()
        {
            return new Product
            {
                Id = Guid.NewGuid().ToString(),
                Name = "iPhone 6",
                Description = "iPhone Inc",
                ProductCategory = "Smart Phone",
                Price = 48000,
                Summary = "NA",
                ImageUrl = "NA"
            };
        }
    }
}
