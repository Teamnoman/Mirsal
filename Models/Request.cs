using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MIRSAL.Models
{
    public class Request
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;
        public string CustomerID { get; set; } = null!;
        public string PolicyID { get; set; } = null!;
        public string Location { get; set; } = null!;
        public string IncidentStartTime { get; set; } = null!;
        public string IncidentEndTime { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string FootageUrl { get; set; } = null!;
        public string Status { get; set; } = null!;
        public DateTime IncidentDate { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}