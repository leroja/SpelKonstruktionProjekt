using GameEngine.Source.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spel.Source.Components
{
    public class ChangeCubeComponent : IComponent
    {
        public bool isTaken { get; set; }
        public float time { get; set; }

        public ChangeCubeComponent()
        {
            time = 0;
            isTaken = false;
        }

    }
}
