using System.Linq.Expressions;
using Catalog.API.Context;
using Catalog.API.Interfaces.Manager;
using Catalog.API.Interfaces.Repository;
using Catalog.API.Models;
using MongoRepo.Repository;

namespace Catalog.API.Repositories
{
    public class ProductRepository : CommonRepository<Product>, IProductRepository
    {
        public ProductRepository():base(new CatalogDbContext())
        {
               
        }
        
    }
}
