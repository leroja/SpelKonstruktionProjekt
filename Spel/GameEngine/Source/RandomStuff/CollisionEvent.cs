using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Source.RandomStuff
{
    /// <summary>
    /// An Event for collisions
    /// </summary>
    public class CollisionEvent : IEvent
    {
        public int entity1 { get; set; }
        public int entity2 { get; set; }
        public GameTime gt { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ent1"> ID of one of the colliding entities </param>
        /// <param name="ent2"> ID of one of the colliding entities </param>
        /// <param name="gt"></param>
        public CollisionEvent(int ent1, int ent2, GameTime gt)
        {
            entity1 = ent1;
            entity2 = ent2;
            this.gt = gt;
        }
    }
}
