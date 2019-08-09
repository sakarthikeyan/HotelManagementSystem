using System;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SunmathiTech.HRMS.Models
{
    public class PaymentType
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("code")]
        public string Code { get; set; }

        [BsonElement("disabled")]
        public bool Disabled { get; set; }

        [BsonElement("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
