using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Microsoft.Extensions.Configuration;

using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

using SunmathiTech.HRMS.Models;

namespace SunmathiTech.HRMS.DAL.MongoDB
{
    public class CreditcardTypeService
    {
        private readonly IMongoCollection<CreditcardType> _collection;

        public CreditcardTypeService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("HotelManagementSystem"));
            var database = client.GetDatabase("hms");
            //var pack = new ConventionPack();
            //pack.Add(new CamelCaseElementNameConvention());
            //ConventionRegistry.Register("camel case", pack, t => false);
            _collection = database.GetCollection<CreditcardType>("creditcardtypes");
        }

        public List<CreditcardType> Get()
        {
            return _collection.Find(item => true).ToList();
        }

        public CreditcardType Get(string id)
        {
            return _collection.Find<CreditcardType>(item => item.Id == id).FirstOrDefault();
        }

        public ExpandoObject Get(int limit, int skip)
        {
            dynamic expandoObj = new ExpandoObject();
            expandoObj.CreditcardTypes = _collection.Find(item => true)
                //.Sort("{Name: 1}")
                .Limit(limit)
                .Skip(skip)
                .ToList();
            expandoObj.Count = GetEstimatedDocumentCount();
            return expandoObj;
        }

        public long GetEstimatedDocumentCount()
        {
            return _collection.EstimatedDocumentCount();
        }

        public CreditcardType Create(CreditcardType itemIn)
        {
            _collection.InsertOne(itemIn);
            return itemIn;
        }

        public void Update(string id, CreditcardType itemIn)
        {
            _collection.ReplaceOne(item => item.Id == id, itemIn);
        }

        public void Remove(CreditcardType itemIn)
        {
            _collection.DeleteOne(item => item.Id == item.Id);
        }

        public void Remove(string id)
        {
            _collection.DeleteOne(item => item.Id == id);
        }
    }
}