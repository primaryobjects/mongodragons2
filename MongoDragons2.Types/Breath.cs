using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDragons2.Types
{
    public class Breath
    {
        public enum BreathType
        {
            Fire,
            Ice,
            Lightning,
            PoisonGas,
            Darkness,
            Light
        };

        public string Name { get; set; }
        public string Description { get; set; }
        public BreathType Type { get; set; }
    }
}
