using Beerfest.Core.Infrastructure.Mongo;
using MongoDB.Bson.Serialization.Attributes;

namespace Beerfest.Core.Entities {

    public class Beer : MongoEntity {

        public string Brewery { get; set; }

        [BsonElement("beer")]
        public string Name { get; set; }
        public double? Abv { get; set; }
        public int? Ibu { get; set; }
        public string Style { get; set; }
        public int? BaScore { get; set; }
        public int? BrosScore { get; set; }
        public double? UntappdScore { get; set; }
        public string ImageUrl { get; set; }
        public string UntappdBeerId { get; set; }

    }

}