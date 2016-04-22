using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Source.Components
{
    // Position Component in 2D
    class PositionComponent : Component
    {
        // Position X
        int x { get; set; }
        // Position Y
        int y { get; set; }

        // Constructor for PositionComponent 
        public PositionComponent(int posX, int posY)
        {
            x = posX;
            y = posY;
        }
    }
}
