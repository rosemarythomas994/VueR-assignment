using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Revv.cars.Shared
{
    
    public class Car
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("image")]
        public string? Image { get; set; }

        [BsonElement("brand")]
        public string? Brand { get; set; }

        [BsonElement("model")]
        public string? Model { get; set; }

        [BsonElement("year")]
        public int Year { get; set; }

        [BsonElement("place")]
        public string? Place { get; set; }

        [BsonElement("number")]
        public int Number { get; set; }

        [BsonElement("date")]
        public string? Date { get; set; }

        [BsonElement("createdAt")]
        public DateTime? CreatedAt { get; set; }

        [BsonElement("updatedAt")]
        public DateTime? UpdatedAt { get; set; }


        [BsonElement("userId")]
        public string? UserId { get; set; }


    }
}
