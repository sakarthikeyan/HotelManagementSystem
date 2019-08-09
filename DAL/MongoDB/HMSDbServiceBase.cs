using MongoDB.Bson;

namespace SunmathiTech.HRMS.DAL.MongoDB
{
    public class HMSDbServiceBase
    {
        readonly protected int MaxLimit = 20;
        protected BsonDocument Filter;
    }
}
