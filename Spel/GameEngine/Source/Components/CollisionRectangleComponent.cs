using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Source.Components
{
    class CollisionRectangleComponent : IComponent
    {
        public Rectangle CollisionRec { get; set; }

        public CollisionRectangleComponent(Rectangle rec)
        {
            this.CollisionRec = rec;
        }
    }
}
