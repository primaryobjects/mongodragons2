using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDragons2.Repository.Interface;
using MongoDragons2.Types;

namespace MongoDragons2.Repository.Concrete
{
    public static class DragonRepository
    {
        private static IRepository<Dragon> _repository = new MongoRepository<Dragon>("Dragon");

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
