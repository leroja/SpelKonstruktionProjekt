using GameEngine.Source.RandomStuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Source.Components
{
    /// <summary>
    /// this is just component for testing collision stuff
    /// </summary>
    public class CollisionHappenedComponent : IComponent
    {
        public List<Collision> collisions { get; set; }
        
        public CollisionHappenedComponent()
        {
            collisions = new List<Collision>();
        }
    }
}
