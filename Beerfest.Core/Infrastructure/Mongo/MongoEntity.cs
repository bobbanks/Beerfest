using System;
using System.Runtime.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Beerfest.Core.Infrastructure.Mongo {

    public interface IMongoEntity<TKey> {
        TKey Id { get; set; }
    }

    public interface IMongoEntity : IMongoEntity<string> { }

    [DataContract]
    [Serializable]
    [BsonIgnoreExtraElements(Inherited = true)]
    public class MongoEntity : IMongoEntity<string>
    {
        [DataMember]
        [BsonRepresentation(BsonType.ObjectId)]
        public virtual string Id { get; set; }

        public BsonDocument ExtraElements { get; set; }
    }
}