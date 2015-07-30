using System.Collections.Generic;
using System.Linq;
using Beerfest.Core.Entities;
using Beerfest.Core.Infrastructure.Mongo;
using MongoDB.Driver;

namespace Beerfest.Core.DataServices {

    
    public interface IBeerRepository : IMongoRepository<Beer> {}

    public class BeerRepository : MongoRepository<Beer>, IBeerRepository {
        public BeerRepository(MongoDatabase database) : base(database, "beers") {}

        
    }

}
