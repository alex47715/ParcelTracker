using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ParcelInfoService.Models
{
    public class ParcelModel
    {
        [BsonElement(elementName: "ParcelId")]
        public string ParcelId { get; set; }
        [BsonElement(elementName: "SenderName")]
        public string SenderName { get; set; }
        [BsonElement(elementName: "RecipientName")]
        public string RecipientName { get; set; }
        [BsonElement(elementName: "Weight")]
        public double Weight { get; set; }
    }
}
