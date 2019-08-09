using System;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SunmathiTech.HRMS.Models
{
    public class Rate
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("categoryId")]
        public string CategoryId { get; set; }

        [BsonElement("classId")]
        public string ClassId { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("basePrice")]
        public Double BasePrice { get; set; }

        [BsonElement("disabled")]
        public bool Disabled { get; set; }

        [BsonElement("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
