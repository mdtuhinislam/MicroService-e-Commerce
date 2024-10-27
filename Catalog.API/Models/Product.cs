using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.API.Models
{
    public class Product
    {
        [BsonId]
        public  string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProductCategory { get; set; }
        //public ProductCategory ProductCategory { get; set; }
        public string Summary { get; set; }
        public string ImageUrl { get; set; }
        public decimal? Price { get; set; }

    }
}
