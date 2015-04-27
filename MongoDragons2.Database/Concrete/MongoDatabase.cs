using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using builder = MongoDB.Driver.Builders;
using MongoDragons2.Database.Interface;
using MongoDragons2.Database.Helpers;

namespace MongoDragons2.Database.Concrete
{
    public class MongoDatabase<T> : IDatabase<T> where T : class, new()
    {
        private static string _connectionString = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
        private static MongoServer _server = new MongoClient(_connectionString).GetServer();
        private string _collectionName;
        private MongoDatabase _db;

        protected MongoCollection<T> _collection
        {
            get
            {
                return _db.GetCollection<T>(_collectionName);
            }
            set
            {
                _collection = value;
            }
        }

        public IQueryable<T> Query
        {
            get
            {
                return _collection.AsQueryable<T>();
            }
            set
            {
                Query = value;
            }
        }

        public MongoDatabase(string collectionName)
        {
            _collectionName = collectionName;
            _db = _server.GetDatabase(MongoUrl.Create(_connectionString).DatabaseName);
        }

        public int Delete(System.Linq.Expressions.Expression<Func<T, bool>> expression)
        {
            int count = 0;

            var items = Query.Where(expression);
            foreach (T item in items)
            {
                if (Delete(item))
                {
                    count++;
                }
            }

            return count;
        }

        public bool Delete(T item)
        {
            ObjectId id = new ObjectId(typeof(T).GetProperty("Id").GetValue(item, null).ToString());
            var query = builder.Query.EQ("_id", id);

            // Remove the object.
            var result = _collection.Remove(query, RemoveFlags.Single);

            return result.DocumentsAffected == 1;
        }

        public void DeleteAll()
        {
            _db.DropCollection(typeof(T).Name);
        }

        public T Single(System.Linq.Expressions.Expression<Func<T, bool>> expression)
        {
            return Query.Where(expression).SingleOrDefault();
        }

        public IQueryable<T> All(int page, int pageSize)
        {
            return PagingExtensions.Page(Query, page, pageSize);
        }

        public bool Add(T item)
        {
            var result = _collection.Save(item);

            return result.Ok;
        }

        public int Add(IEnumerable<T> items)
        {
            int count = 0;

            foreach (T item in items)
            {
                if (Add(item))
                {
                    count++;
                }
            }

            return count;
        }

        /// <summary>
        /// MongoDb query method for searching using the native MongoDb expression builder.
        /// Example:
        /// var query = Query.Matches("Name", new BsonRegularExpression(keyword, "i"));
        /// var result = ((MongoDatabase<Person>)_db).Expression(query, SortBy.Descending("MyField"), 1, 100);
        /// </summary>
        public MongoCursor<T> Expression(IMongoQuery query, IMongoSortBy sort, int page, int pageSize)
        {
            var cursor = _collection.Find(query).SetSortOrder(sort);

            return PagingExtensions.Page(cursor, page, pageSize);
        }
    }
}
