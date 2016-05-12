using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Source.Components
{
    public class CollisionComponent : IComponent
    {
        public bool isPixelPerfectCompat { get; set; }
        //@temp, @TODO
        public bool Stuff { get; set; }

        public CollisionComponent(bool active)
        {
            this.isPixelPerfectCompat = active;
        }
    }
}
