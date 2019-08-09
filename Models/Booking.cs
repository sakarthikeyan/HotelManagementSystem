using System;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SunmathiTech.HRMS.Models
{
    public class Booking
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("roomId")]
        public string RoomId { get; set; }

        [BsonElement("categoryId")]
        public string CategoryId { get; set; }

        [BsonElement("classId")]
        public string ClassId { get; set; }

        //[BsonElement("name")]
        //public string Name { get; set; }

        //[BsonElement("code")]
        //public string Code { get; set; }

        [BsonElement("customerId")]
        public string CustomerId { get; set; }

        [BsonElement("checkinDate")]
        public string CheckinDate { get; set; }

        [BsonElement("checkoutDate")]
        public string CheckoutDate { get; set; }

        [BsonElement("basePrice")]
        public Double BasePrice { get; set; }

        [BsonElement("disabled")]
        public bool Disabled { get; set; }

        [BsonElement("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
