using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Source.Components
{
    //PowerUpComponent
    public class PowerUppComponent : IComponent
    {
        // för test
        int type;

        public PowerUppComponent(int type)
        {
            this.type = type;
        }
    }
}
