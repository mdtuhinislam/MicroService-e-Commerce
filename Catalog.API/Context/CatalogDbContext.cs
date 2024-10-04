using MongoRepo.Context;

namespace Catalog.API.Context
{
    public class CatalogDbContext : ApplicationDbContext
    {
        
        static string connectionString = GetJsonData("Catalog.API");

        static string databaseName = GetJsonData("DatabaseName");
        public CatalogDbContext() : base(connectionString, databaseName)
        {
        }

        private static string GetJsonData(string key)
        {
            return new ConfigurationBuilder().
            SetBasePath(Directory.GetCurrentDirectory()).
            AddJsonFile("appsetings.json", true, true).
            Build().
            GetConnectionString(key);
        }
    }
}
