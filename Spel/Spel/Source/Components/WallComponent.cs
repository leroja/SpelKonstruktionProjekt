using GameEngine.Source.Components;
using Spel.Source.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spel.Source.Components
{
    class WallComponent : IComponent
    {
        public Wall wall;

        public WallComponent(Wall wall)
        {
            this.wall = wall;

        }
    }
}
