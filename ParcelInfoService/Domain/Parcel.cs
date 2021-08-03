using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcelInfoService.Domain
{
    public class Parcel
    {
        [BsonId]
        public ObjectId _id { get; set; }
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
