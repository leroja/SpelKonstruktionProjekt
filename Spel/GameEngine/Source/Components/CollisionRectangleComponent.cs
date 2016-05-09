using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Source.Components
{
    public class CollisionRectangleComponent : IComponent
    {
        public Rectangle CollisionRec;

        public CollisionRectangleComponent(Rectangle rec)
        {
            this.CollisionRec = rec;
        }
    }
}
