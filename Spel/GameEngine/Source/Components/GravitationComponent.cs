using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Source.Components
{
    // GravitationComponent for Gravity
    class GravitationComponent
    {
        // Flot representing Gravity
        float gravity { get; set; }
         
        // Constructor form gravitationComponent
        public GravitationComponent(float gravity)
        {
            this.gravity = gravity;
        }
    }
}
