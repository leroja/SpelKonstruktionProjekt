using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Source.Components;

namespace Spel.Source.Components
{
    //PowerUpComponent
    public class PowerUppComponent : IComponent
    {
        // Type
        public int type { get; set; }

        public PowerUppComponent(int type)
        {
            this.type = type;
        }
    }
}
