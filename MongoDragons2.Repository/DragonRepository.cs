using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDragons2.Types;
using MongoDragons2.Database.Interface;
using MongoDragons2.Database.Concrete;
using MongoDB.Driver.Builders;
using MongoDB.Bson;

namespace MongoDragons2.Repository
{
    public static class DragonRepository
    {
        private static IDatabase<Dragon> _db = new MongoDatabase<Dragon>("Dragon");

        public static Dragon Spawn()
        {
            Dragon dragon = new Dragon() { Name = "Test", Age = 10, Gold = 100 };

            // Save object.
            if (!_db.Add(dragon))
            {
                dragon = null;
            }

            return dragon;
        }

        public static bool Remove(Dragon dragon)
        {
            return _db.Delete(dragon);
        }

        public static IEnumerable<Dragon> ToList()
        {
            return _db.Query.AsEnumerable();
        }

        public static IEnumerable<Dragon> Search(string keyword)
        {
            // Example searching with a MongoDb expression builder query. Normally, you can use LINQ to query (MongoDb expressions are helpful for cases where the query is not supported by LINQ).
            var query = Query.Matches("Name", new BsonRegularExpression(keyword, "i"));

            return ((MongoDatabase<Dragon>)_db).Expression(query, SortBy.Ascending("Name"), 1, 100);
        }
    }
}
