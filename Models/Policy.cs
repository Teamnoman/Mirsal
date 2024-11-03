using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MIRSAL.Models
{
    public class Policy
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;
        public string CustomerID { get; set; } = null!;
        public string VehicleID { get; set; } = null!;
        public string BranchAddress { get; set; } = null!;
        public string PolicyStatus { get; set; } = null!;
        public DateTime CreatedDateTime { get; set; }
    }
}