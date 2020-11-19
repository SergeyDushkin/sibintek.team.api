using System;
using MongoDB.Bson.Serialization.Attributes;
using sibintek.db.mongodb;

namespace sibintek.team
{
    public class Base : Entity
    {
        public string Text { get; set; }

        [BsonElement]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime Timestamp { get; set; } 
    }
}
