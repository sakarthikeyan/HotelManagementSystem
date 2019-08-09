using MongoDB.Driver;
using MongoDbGenericRepository;

namespace SunmathiTech.HRMS.DAL.MongoDB.Infrasructure
{
    public interface IBaseMongoDBRepository : IBaseMongoRepository
    {
        void DropTestCollection<TDocument>();
        void DropTestCollection<TDocument>(string partitionKey);
    }

    public class BaseMongoDBRepository : BaseMongoRepository, IBaseMongoDBRepository
    {
        public BaseMongoDBRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase)
        {
        }

        public BaseMongoDBRepository(string connectionString, string databaseName) : base(connectionString, databaseName)
        {
        }

        public void DropTestCollection<TDocument>()
        {
            MongoDbContext.DropCollection<TDocument>();
        }

        public void DropTestCollection<TDocument>(string partitionKey)
        {
            MongoDbContext.DropCollection<TDocument>(partitionKey);
        }
    }
}
