using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GameEngine.Source.Components
{
    // Position Component in 2D
    class PositionComponent : Component
    {
        // Position
        Vector2 position;

        // Constructor for PositionComponent 
        public PositionComponent(Vector2 startPosition)
        {
            position = startPosition;
        }
    }
}
