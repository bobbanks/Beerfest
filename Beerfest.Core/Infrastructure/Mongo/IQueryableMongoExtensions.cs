using System.Diagnostics;
using System.Linq;
using MongoDB.Driver.Linq;

namespace Beerfest.Core.Infrastructure.Mongo {

    public static class IQueryableMongoExtensions
    {

        public static string ToMongoQueryText<TQueryable>(this IQueryable<TQueryable> query)
        {
            return (query as MongoQueryable<TQueryable>).GetMongoQuery().ToString();
        }

        public static void DebugWriteMongoQueryText<TQuerable>(this IQueryable<TQuerable> query)
        {
            var mongoQueryable = (query as MongoQueryable<TQuerable>);
            var translatedQuery = MongoQueryTranslator.Translate(mongoQueryable);
            var selectQuery = translatedQuery as SelectQuery;

            Debug.WriteLine("QUERY: " + mongoQueryable.ToMongoQueryText());
            if (selectQuery != null)
            {
                Debug.WriteLine("TAKE: " + selectQuery.Take.GetValueOrDefault(0));
                Debug.WriteLine("SKIP: " + selectQuery.Skip.GetValueOrDefault(0));
            }
        }

    }

}