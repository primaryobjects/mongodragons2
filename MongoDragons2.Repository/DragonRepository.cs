using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDragons2.Types;
using MongoDragons2.Database.Interface;
using MongoDragons2.Database.Concrete;

namespace MongoDragons2.Repository
{
    public static class DragonRepository
    {
        private static IDatabase<Dragon> _repository = new MongoDatabase<Dragon>("Dragon");

        public static Dragon Spawn()
        {
            Dragon dragon = new Dragon() { Name = "Test", Age = 10, Gold = 100 };

            // Save object.
            if (!_repository.Add(dragon))
            {
                dragon = null;
            }

            return dragon;
        }

        public static bool Remove(Dragon dragon)
        {
            return _repository.Delete(dragon);
        }

        public static IEnumerable<Dragon> ToList()
        {
            return _repository.Query.AsEnumerable();
        }
    }
}
