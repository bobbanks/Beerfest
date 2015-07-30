using System.Configuration;
using MongoDB.Driver;
using Ninject.Modules;

namespace Beerfest.Core.Infrastructure.Mongo {

    public class MongoNinjectModule : NinjectModule {
        public override void Load() {
            Bind<MongoDatabase>().ToMethod(c => new MongoDatabaseFactory().Create(ConfigurationManager.ConnectionStrings["mongo"].ConnectionString)).InSingletonScope();
        } 
    }
}