using System;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SunmathiTech.HRMS.Models
{
    public class Category
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("noofbeds")]
        public int NoOfBeds { get; set; }

        [BsonElement("disabled")]
        public bool Disabled { get; set; }

        [BsonElement("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
