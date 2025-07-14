using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RevvCar.Models
{
    public class Car
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; } = string.Empty;

        [BsonElement("createdAt")]
        public string CreatedAt { get; set; } = string.Empty;

        [BsonElement("updatedAt")]
        public string UpdatedAt { get; set; } = string.Empty;

        [BsonElement("image")]
        public string Image { get; set; } = string.Empty;

        [BsonElement("brand")]
        public string Brand { get; set; } = string.Empty;

        [BsonElement("model")]
        public string Model { get; set; } = string.Empty;

        [BsonElement("year")]
        public int Year { get; set; }

        [BsonElement("place")]
        public string Place { get; set; } = string.Empty;

        [BsonElement("number")]
        public int Number { get; set; }

        [BsonElement("date")]
        public string Date { get; set; } = string.Empty;

        [BsonElement("userId")]
        public string UserId { get; set; } = string.Empty;
    }
}