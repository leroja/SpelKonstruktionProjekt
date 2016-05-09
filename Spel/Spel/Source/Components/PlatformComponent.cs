using GameEngine.Source.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spel.Source.Components
{
    class PlatformComponent : IComponent
    {
        public Rectangle TopRec;
        public Rectangle LeftRec;
        public Rectangle BottomRec;
        public Rectangle RightRec;

        public PlatformComponent(Vector2 pos, int width, int height)
        {
            TopRec = new Rectangle((int)pos.X, (int)pos.Y, width, 5);
            LeftRec = new Rectangle((int)pos.X, (int)pos.Y + 5, 5, height - 10);
            BottomRec = new Rectangle((int)pos.X, height - 5, width, 5);
            RightRec = new Rectangle(width - 5, (int)pos.Y + 5, 5, height - 10);
        }
    }
}
