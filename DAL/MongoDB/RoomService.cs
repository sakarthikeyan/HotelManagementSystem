﻿using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Microsoft.Extensions.Configuration;

using MongoDB.Driver;

using SunmathiTech.HRMS.Models;

namespace SunmathiTech.HRMS.DAL.MongoDB
{
    public class RoomService : HMSDbServiceBase
    {
        private readonly IMongoCollection<Room> _collection;
        private readonly IMongoCollection<Category> _collection2;
        //private readonly IMongoCollection<Class> _collection3;

        public RoomService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("HotelManagementSystem"));
            var database = client.GetDatabase("hms");
            _collection = database.GetCollection<Room>("rooms");
            _collection2 = database.GetCollection<Category>("categories");
        }

        public List<Room> Get()
        {
            return _collection.Find(item => true).ToList();
        }

        public Room Get(string id)
        {
            return _collection.Find<Room>(item => item.Id == id).FirstOrDefault();
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
                expandoObj.Rooms = _collection.Find(filter)
                    .ToList();
                expandoObj.Count = expandoObj.Rooms.Count;
            }
            else
            {
                filter = "{}";
                expandoObj.Count = GetEstimatedDocumentCount();
            }
            expandoObj.Rooms = _collection.Find(filter)
                //.Sort("{Name: 1}")
                .Limit(limit)
                .Skip(skip)
                .ToList();
            expandoObj.Categories = _collection2.Find(item => true)
                .ToList();
            return expandoObj;
        }

        public long GetEstimatedDocumentCount()
        {
            return _collection.EstimatedDocumentCount();
        }

        public Room Create(Room itemIn)
        {
            _collection.InsertOne(itemIn);
            return itemIn;
        }

        public void Update(string id, Room itemIn)
        {
            _collection.ReplaceOne(item => item.Id == id, itemIn);
        }

        public void Remove(Room itemIn)
        {
            _collection.DeleteOne(item => item.Id == item.Id);
        }

        public void Remove(string id)
        {
            _collection.DeleteOne(item => item.Id == id);
        }
    }
}