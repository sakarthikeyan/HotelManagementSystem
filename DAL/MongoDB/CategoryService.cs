using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Microsoft.Extensions.Configuration;

using MongoDB.Driver;

using SunmathiTech.HRMS.Models;

namespace SunmathiTech.HRMS.DAL.MongoDB
{
    public class CategoryService : HMSDbServiceBase
    {
        private readonly IMongoCollection<Category> _collection;

        public CategoryService(IConfiguration config)
        {
            try
            {
                var client = new MongoClient(config.GetConnectionString("HotelManagementSystem"));
                var database = client.GetDatabase("hms");
                _collection = database.GetCollection<Category>("categories");
            }
            catch(Exception e)
            {
                //Do nothing.
                throw e;
            }
        }

        public List<Category> Get()
        {
            return _collection.Find(item => true).ToList();
        }

        public Category Get(string id)
        {
            return _collection.Find<Category>(item => item.Id == id).FirstOrDefault();
        }

        public ExpandoObject Get(int limit, int skip)
        {
            dynamic expandoObj = new ExpandoObject();
            expandoObj.Categories = _collection.Find(
                    item => true,
                    new FindOptions() {
                        Collation = new Collation(
                            "en", 
                            strength: CollationStrength.Secondary,
                            caseLevel: false
                            )
                    })
                .Sort("{Name: 1}")
                .Limit(Math.Min(limit, this.MaxLimit))
                .Skip(skip)
                .ToList();
            expandoObj.Count = GetEstimatedDocumentCount();
            return expandoObj;
        }

        public long GetEstimatedDocumentCount()
        {
            return _collection.EstimatedDocumentCount();
        }

        public Category Create(Category itemIn)
        {
            _collection.InsertOne(itemIn);
            return itemIn;
        }

        public void Update(string id, Category itemIn)
        {
            _collection.ReplaceOne(item => item.Id == id, itemIn);
        }

        public void Remove(Category itemIn)
        {
            _collection.DeleteOne(item => item.Id == item.Id);
        }

        public void Remove(string id)
        {
            _collection.DeleteOne(item => item.Id == id);
        }
    }
}