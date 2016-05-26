using GameEngine.Source.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spel.Source.Components
{
    class OnFloorComponent : IComponent
    {
        public bool active { get; set; }

        public OnFloorComponent()
        {
            active = false;
        }
    }
}
