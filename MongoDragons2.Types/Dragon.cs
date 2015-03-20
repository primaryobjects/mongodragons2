using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDragons2.Types
{
    [BsonIgnoreExtraElements]
    public class Dragon
    {
        public ObjectId Id { get; private set; }
        /// <summary>
        /// Required for serialization of Id between javascript and C# controller. This restores ObjectId Id.
        /// </summary>
        public string IdString {
            get
            {
                return Id.ToString();
            }            
            set
            {
                Id = new ObjectId(value);
            }
        }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Description { get; set; }
        public int Gold { get; set; }
        public int MaxHP { get; set; }
        public int HP { get; set; }
        public Breath Weapon { get; set; }
        public DateTime DateBorn { get; set; }
        public DateTime? DateDied { get; set; }

        public Dragon()
        {
            DateBorn = DateTime.Now;
        }
    }
}
