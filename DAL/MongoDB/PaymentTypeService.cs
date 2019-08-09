using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Microsoft.Extensions.Configuration;

using MongoDB.Driver;

using SunmathiTech.HRMS.Models;

namespace SunmathiTech.HRMS.DAL.MongoDB
{
    public class PaymentTypeService
    {
        private readonly IMongoCollection<PaymentType> _collection;

        public PaymentTypeService(IConfiguration config)
        {
            try
            {
                var client = new MongoClient(config.GetConnectionString("HotelManagementSystem"));
                var database = client.GetDatabase("hms");
                _collection = database.GetCollection<PaymentType>("paymenttypes");
            }
            catch(Exception e)
            {
                //Do nothing.
                throw e;
            }
        }

        public List<PaymentType> Get()
        {
            return _collection.Find(item => true).ToList();
        }

        public PaymentType Get(string id)
        {
            return _collection.Find<PaymentType>(item => item.Id == id).FirstOrDefault();
        }

        public ExpandoObject Get(int limit, int skip)
        {
            dynamic expandoObj = new ExpandoObject();
            expandoObj.PaymentTypes = _collection.Find(
                    item => true,
                    new FindOptions() {
                        Collation = new Collation(
                            "en", 
                            strength: CollationStrength.Secondary,
                            caseLevel: false
                            )
                    })
                .Sort("{Name: 1}")
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

        public PaymentType Create(PaymentType itemIn)
        {
            _collection.InsertOne(itemIn);
            return itemIn;
        }

        public void Update(string id, PaymentType itemIn)
        {
            _collection.ReplaceOne(item => item.Id == id, itemIn);
        }

        public void Remove(PaymentType itemIn)
        {
            _collection.DeleteOne(item => item.Id == item.Id);
        }

        public void Remove(string id)
        {
            _collection.DeleteOne(item => item.Id == id);
        }
    }
}