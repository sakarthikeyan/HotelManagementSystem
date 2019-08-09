using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Microsoft.Extensions.Configuration;

using MongoDB.Driver;

using SunmathiTech.HRMS.Models;

namespace SunmathiTech.HRMS.DAL.MongoDB
{
    public class ClassService
    {
        private readonly IMongoCollection<Class> _collection;

        public ClassService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("HotelManagementSystem"));
            var database = client.GetDatabase("hms");
            _collection = database.GetCollection<Class>("classes");
        }

        public List<Class> Get()
        {
            return _collection.Find(item => true).ToList();
        }

        public Class Get(string id)
        {
            return _collection.Find<Class>(item => item.Id == id).FirstOrDefault();
        }

        public ExpandoObject Get(int limit, int skip)
        {
            dynamic expandoObj = new ExpandoObject();
            expandoObj.Classes = _collection.Find(item => true)
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

        public Class Create(Class itemIn)
        {
            _collection.InsertOne(itemIn);
            return itemIn;
        }

        public void Update(string id, Class itemIn)
        {
            _collection.ReplaceOne(item => item.Id == id, itemIn);
        }

        public void Remove(Class itemIn)
        {
            _collection.DeleteOne(item => item.Id == item.Id);
        }

        public void Remove(string id)
        {
            _collection.DeleteOne(item => item.Id == id);
        }
    }
}