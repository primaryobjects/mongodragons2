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
        private static MongoClient _client = new MongoClient(_connectionString);
        private string _collectionName;
        private IMongoDatabase _db;

        protected IMongoCollection<T> _collection
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
            _db = _client.GetDatabase(MongoUrl.Create(_connectionString).DatabaseName);
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
            var filter = Builders<T>.Filter.Eq("Id", id);

            // Remove the object.
            var result = _collection.DeleteOne(filter);

            return result.DeletedCount == 1;
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
            _collection.InsertOne(item);

            return true;
        }

        public int Add(IEnumerable<T> items)
        {
            _collection.InsertMany(items);

            return items.Count();
        }
    }
}
