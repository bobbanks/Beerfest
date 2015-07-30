using System;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace Beerfest.Core.Infrastructure.Mongo {

    public class MongoDatabaseFactory {

        public MongoDatabaseFactory() {
            BsonSerializer.RegisterSerializer(typeof(DateTime), new DateTimeSerializer(DateTimeKind.Local));
        }

        public MongoDatabase Create(string mongoUri) {

            var client = new MongoClient(mongoUri);
            
            var server = client.GetServer();
            server.Connect();

            var conventionPack = new ConventionPack { new CamelCaseElementNameConvention() };
            ConventionRegistry.Register("Conventions", conventionPack, t => true);

            var dbName = new MongoUrl(mongoUri).DatabaseName;
            return server.GetDatabase(dbName);
        }
    }

}