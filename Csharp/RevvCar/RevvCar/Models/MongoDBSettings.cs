namespace Revv_Cars.Model
{
    public class MongoDBSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string UsersCollection { get; set; } = null!;
        public string CarsCollection { get; set; } = null!;
    }
}