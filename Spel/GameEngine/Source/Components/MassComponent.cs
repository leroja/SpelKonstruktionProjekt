using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Source.Components
{
    public class MassComponent : IComponent
    {
        public float mass { get; set; }

        public MassComponent(float mass)
        {
            this.mass = mass;
        }
    }
}
