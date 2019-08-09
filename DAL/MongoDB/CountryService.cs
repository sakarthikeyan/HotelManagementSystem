using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Microsoft.Extensions.Configuration;

using MongoDB.Driver;

using SunmathiTech.HRMS.Models;

namespace SunmathiTech.HRMS.DAL.MongoDB
{
    public class CountryService
    {
        private readonly IMongoCollection<Country> _collection;

        public CountryService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("HotelManagementSystem"));
            var database = client.GetDatabase("hms");
            _collection = database.GetCollection<Country>("countries");
        }

        public List<Country> Get()
        {
            return _collection.Find(item => true).ToList();
        }

        public Country Get(string id)
        {
            return _collection.Find<Country>(item => item.Id == id).FirstOrDefault();
        }

        public ExpandoObject Get(int limit, int skip)
        {
            dynamic expandoObj = new ExpandoObject();
            expandoObj.Countries = _collection.Find(item => true)
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

        public Country Create(Country itemIn)
        {
            _collection.InsertOne(itemIn);
            return itemIn;
        }

        public void Update(string id, Country itemIn)
        {
            _collection.ReplaceOne(item => item.Id == id, itemIn);
        }

        public void Remove(Country itemIn)
        {
            _collection.DeleteOne(item => item.Id == item.Id);
        }

        public void Remove(string id)
        {
            _collection.DeleteOne(item => item.Id == id);
        }
    }
}