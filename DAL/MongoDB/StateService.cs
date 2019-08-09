using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Microsoft.Extensions.Configuration;

using MongoDB.Driver;

using SunmathiTech.HRMS.Models;

namespace SunmathiTech.HRMS.DAL.MongoDB
{
    public class StateService
    {
        private readonly IMongoCollection<State> _collection;
        private readonly IMongoCollection<Country> _collection2;

        public StateService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("HotelManagementSystem"));
            var database = client.GetDatabase("hms");
            _collection = database.GetCollection<State>("states");
            _collection2 = database.GetCollection<Country>("countries");
        }

        public List<State> Get()
        {
            return _collection.Find(item => true).ToList();
        }

        public State Get(string id)
        {
            return _collection.Find<State>(item => item.Id == id).FirstOrDefault();
        }

        public ExpandoObject Get(int limit, int skip)
        {
            return Get(limit, skip, string.Empty);
            //dynamic expandoObj = new ExpandoObject();
            //expandoObj.States = _collection.Find(item => true)
            //    //.Sort("{Name: 1}")
            //    .Limit(limit)
            //    .Skip(skip)
            //    .ToList();
            //expandoObj.Count = GetEstimatedDocumentCount();
            //expandoObj.Countries = _collection2.Find(item => true)
            //    .ToList();
            //return expandoObj;
        }

        public ExpandoObject Get(int limit, int skip, string filter)
        {
            dynamic expandoObj = new ExpandoObject();
            if (!string.IsNullOrWhiteSpace(filter))
            {
                expandoObj.Rooms = _collection.Find(filter)
                    .ToList();
                expandoObj.Count = expandoObj.Rooms.Count;
            }
            else
            {
                filter = "{}";
                expandoObj.Count = GetEstimatedDocumentCount();
            }
            expandoObj.States = _collection.Find(filter)
                //.Sort("{Name: 1}")
                .Limit(limit)
                .Skip(skip)
                .ToList();
            expandoObj.Countries = _collection2.Find(item => true)
                .ToList();
            return expandoObj;
        }

        public long GetEstimatedDocumentCount()
        {
            return _collection.EstimatedDocumentCount();
        }

        public State Create(State itemIn)
        {
            _collection.InsertOne(itemIn);
            return itemIn;
        }

        public void Update(string id, State itemIn)
        {
            _collection.ReplaceOne(item => item.Id == id, itemIn);
        }

        public void Remove(State itemIn)
        {
            _collection.DeleteOne(item => item.Id == item.Id);
        }

        public void Remove(string id)
        {
            _collection.DeleteOne(item => item.Id == id);
        }
    }
}