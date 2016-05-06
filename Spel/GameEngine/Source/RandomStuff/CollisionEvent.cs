using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Source.RandomStuff
{
    public class CollisionEvent : IEvent
    {
        public int entity1 { get; set; }
        public int entity2 { get; set; }

        public CollisionEvent(int ent1, int ent2)
        {
            entity1 = ent1;
            entity2 = ent2;
        }
    }
}
