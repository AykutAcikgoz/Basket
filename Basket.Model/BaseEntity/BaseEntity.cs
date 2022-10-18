using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Basket.Entity
{
    public abstract class BaseEntity : IEntity<string>
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}

