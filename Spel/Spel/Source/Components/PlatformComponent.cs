using GameEngine.Source.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spel.Source.Components
{
    /// <summary>
    /// A component for platforms
    /// </summary>
    class PlatformComponent : IComponent
    {
        public Rectangle TopRec;
        public Rectangle LeftRec;
        public Rectangle BottomRec;
        public Rectangle RightRec;

        public PlatformComponent(Vector2 pos, int width, int height)
        {
            // @TODO kolla så att rektanglarna stämmer
            TopRec = new Rectangle((int)pos.X +5, (int)pos.Y, width-10, 5);
            LeftRec = new Rectangle((int)pos.X, (int)pos.Y, 5, height - 5);
            BottomRec = new Rectangle((int)pos.X+5, height - 5, width-10, 5);
            RightRec = new Rectangle(width - 5, (int)pos.Y, 5, height - 5);
        }


        public void RedoRecs(Vector2 pos, int width, int height)
        {
            TopRec = new Rectangle((int)pos.X + 5, (int)pos.Y, width - 10, 5);
            LeftRec = new Rectangle((int)pos.X, (int)pos.Y, 5, height - 5);
            BottomRec = new Rectangle((int)pos.X + 5, height - 5, width - 10, 5);
            RightRec = new Rectangle(width - 5, (int)pos.Y, 5, height - 5);
        }
    }
}
