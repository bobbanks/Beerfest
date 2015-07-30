using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;

namespace Beerfest.Core.Infrastructure.Mongo {

    public interface IMongoRepository<TEntity> : IMongoRepository<TEntity, string> where TEntity : MongoEntity {

    }

    public interface IMongoRepository<TEntity, TKey> where TEntity : IMongoEntity<TKey> {

        MongoCollection Collection { get; }

        void CollectionUpdate(IMongoQuery query, IMongoUpdate update, UpdateFlags updateFlags);

        IList<T> MapReduce<T>(string map, string reduce);

        IList<T> MapReduce<T>(IMongoQuery query, string map, string reduce);

        TEntity Get(TKey id);

        IEnumerable<TEntity> Get(IEnumerable<TKey> id);

        IEnumerable<TEntity> GetMany<TKey2>(string field, IEnumerable<TKey2> values);

        TEntity GetOne(Expression<Func<TEntity, bool>> predicate);

        TEntity GetSingle(Expression<Func<TEntity, bool>> predicate);

        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);

        IEnumerable<TEntity> GetAll();

        TEntity Add(TEntity entity);

        void Add(IEnumerable<TEntity> entities);

        TEntity AddOrUpdate(TEntity entity);

        void AddOrUpdate(IEnumerable<TEntity> entities);

        void Delete(TKey id);

        void Delete(TEntity entity);

        void Delete(Expression<Func<TEntity, bool>> predicate);

        long Count();

        bool Exists(Expression<Func<TEntity, bool>> predicate);

    }

    public class MongoRepository<TEntity, TKey> : IMongoRepository<TEntity, TKey> where TEntity : IMongoEntity<TKey> {
        private readonly MongoCollection _collection;

        public MongoRepository(MongoDatabase database, string collection)
        {
            _collection = database.GetCollection<TEntity>(collection);
        }

        public MongoCollection Collection {
            get { return _collection;  }
        }

        public void CollectionUpdate(IMongoQuery query, IMongoUpdate update, UpdateFlags updateFlags) {
//            Debug.WriteLine(string.Format("Update<{0}>", typeof(TEntity).ToString()));
//            Debug.WriteLine(string.Format("- QUERY: {0}", query.ToString()));
//            Debug.WriteLine(string.Format("- UPDATE: {0}", update.ToString()));
            Collection.Update(query, update, updateFlags);
        }

        public IList<T> MapReduce<T>(string map, string reduce) {
            return MapReduce<T>(null, map, reduce);
        }

        public IList<T> MapReduce<T>(IMongoQuery query, string map, string reduce)
        {
            var results = _collection.MapReduce(new MapReduceArgs {Query = query, MapFunction = map, ReduceFunction = reduce});
            return results.GetResultsAs<T>().ToList();
        }

        public virtual TEntity Get(TKey id)
        {
            if (typeof(TEntity).IsSubclassOf(typeof(MongoEntity)))
            {
                return _collection.FindOneByIdAs<TEntity>(new ObjectId(id as string));
            }

            return _collection.FindOneByIdAs<TEntity>(BsonValue.Create(id));
        }

        public virtual IEnumerable<TEntity> Get(IEnumerable<TKey> id) {
            IMongoQuery query;
            if (typeof (TEntity).IsSubclassOf(typeof (MongoEntity))) {
                query = Query.In("_id", id.Select(x => new BsonObjectId(new ObjectId(x as string))));
            } else {
                var bsonValues = id.Select(x => BsonValue.Create(x)).ToList();
                query = Query.In("_id", bsonValues);
            }
            
            return _collection.FindAs<TEntity>(query);
        }

        public virtual IEnumerable<TEntity> GetMany<TKey2>(string field, IEnumerable<TKey2> values)
        {
            var bsonValues = values.Select(x => BsonValue.Create(x)).ToList();
            IMongoQuery query = Query.In(field, bsonValues);
            return _collection.FindAs<TEntity>(query);
        }

        public virtual TEntity GetOne(Expression<Func<TEntity, bool>> predicate)
        {
            return _collection.AsQueryable<TEntity>().FirstOrDefault(predicate);
        }

        public virtual TEntity GetSingle(Expression<Func<TEntity, bool>> predicate)
        {
            return _collection.AsQueryable<TEntity>().SingleOrDefault(predicate);
        }

        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return _collection.AsQueryable<TEntity>().Where(predicate);
        }

        public virtual IEnumerable<TEntity> GetAll() {
            return _collection.FindAllAs<TEntity>();
        }

        public virtual TEntity Add(TEntity entity)
        {
            _collection.Insert(entity);
            return entity;
        }

        public virtual void Add(IEnumerable<TEntity> entities)
        {
            _collection.InsertBatch(entities);
        }

        public virtual TEntity AddOrUpdate(TEntity entity)
        {
            _collection.Save(entity);
            return entity;
        }

        public virtual void AddOrUpdate(IEnumerable<TEntity> entities)
        {
            foreach (TEntity entity in entities)
            {
                _collection.Save(entity);
            }
        }

        public virtual void Delete(TKey id)
        {
            _collection.Remove(typeof (TEntity).IsSubclassOf(typeof (MongoEntity))
                ? Query.EQ("_id", new ObjectId(id as string))
                : Query.EQ("_id", BsonValue.Create(id)));
        }

        public virtual void Delete(TEntity entity)
        {
            Delete(entity.Id);
        }

        public virtual void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            foreach (TEntity entity in _collection.AsQueryable<TEntity>().Where(predicate))
            {
                Delete(entity.Id);
            }
        }

        public virtual long Count()
        {
            return _collection.Count();
        }

        public virtual bool Exists(Expression<Func<TEntity, bool>> predicate)
        {
            return _collection.AsQueryable<TEntity>().Any(predicate);
        }

    }

    public class MongoRepository<TEntity> : MongoRepository<TEntity, string> where TEntity : IMongoEntity<string>
    {
        public MongoRepository(MongoDatabase database, string collection) : base(database, collection) { }
    }

}