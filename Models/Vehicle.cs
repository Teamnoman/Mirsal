using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MIRSAL.Models
{
    public class Vehicle
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;
        public string CustomerID { get; set; } = null!;
        public string Model { get; set; } = null!;
        public int Year { get; set; }
        public string ChassisNumber { get; set; } = null!;
        public string RegistrationNumber { get; set; } = null!;
    }
}