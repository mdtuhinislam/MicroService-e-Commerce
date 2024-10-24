using MongoRepo.Context;

namespace Catalog.API.Context
{
    public class CatalogDbContext : ApplicationDbContext
    {
        static IConfiguration _configuration = new ConfigurationBuilder().
            SetBasePath(Directory.GetCurrentDirectory()).
            AddJsonFile("appsettings.json", true, true).
            Build();

        static string connectionString = _configuration.GetConnectionString("Catalog.API");

        static string databaseName = _configuration.GetValue<string>("DatabaseName");
        public CatalogDbContext() : base(connectionString, databaseName)
        {
        }
    }
}
