using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Microsoft.Extensions.Configuration;

using MongoDB.Driver;

using SunmathiTech.HRMS.Models;

namespace SunmathiTech.HRMS.DAL.MongoDB
{
    public class BookingService : HMSDbServiceBase
    {
        private readonly IMongoCollection<Booking> _collection;
        private readonly IMongoCollection<Category> _collection2;
        private readonly IMongoCollection<Class> _collection3;
        private readonly IMongoCollection<Room> _collection4;
        private readonly IMongoCollection<Rate> _collection5;

        public BookingService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("HotelManagementSystem"));
            var database = client.GetDatabase("hms");
            _collection = database.GetCollection<Booking>("bookings");
            _collection2 = database.GetCollection<Category>("categories");
            _collection3 = database.GetCollection<Class>("classes");
            _collection4 = database.GetCollection<Room>("rooms");
            _collection5 = database.GetCollection<Rate>("rates");
        }

        public List<Booking> Get()
        {
            return _collection.Find(item => true).ToList();
        }

        public Booking Get(string id)
        {
            return _collection.Find<Booking>(item => item.Id == id).FirstOrDefault();
        }

        public ExpandoObject Get(int limit, int skip)
        {
            return Get(limit, skip, string.Empty);
        }

        public ExpandoObject Get(int limit, int skip, string filter)
        {
            dynamic expandoObj = new ExpandoObject();
            if (!string.IsNullOrWhiteSpace(filter))
            {
                expandoObj.Bookings = _collection.Find(filter)
                    .ToList();
                expandoObj.BookingsCount = expandoObj.Bookings.Count;
            }
            else
            {
                filter = "{}";
                expandoObj.BookingsCount = GetEstimatedDocumentCount();
            }
            expandoObj.Bookings = _collection.Find(filter)
                .Limit(limit)
                .Skip(skip)
                .ToList();
            expandoObj.Categories = _collection2.Find(item => true)
                .ToList();
            expandoObj.Classes = _collection3.Find(item => true)
                .ToList();
            expandoObj.Rooms = _collection4.Find(filter)
                .ToList();
            expandoObj.RoomsCount = expandoObj.Rooms.Count;
            expandoObj.Rates = _collection5.Find(item => true)
                .ToList();
            return expandoObj;
        }

        public long GetEstimatedDocumentCount()
        {
            return _collection.EstimatedDocumentCount();
        }

        public Booking Create(Booking itemIn)
        {
            _collection.InsertOne(itemIn);
            return itemIn;
        }

        public void Update(string id, Booking itemIn)
        {
            _collection.ReplaceOne(item => item.Id == id, itemIn);
        }

        public void Remove(Booking itemIn)
        {
            _collection.DeleteOne(item => item.Id == item.Id);
        }

        public void Remove(string id)
        {
            _collection.DeleteOne(item => item.Id == id);
        }
    }
}