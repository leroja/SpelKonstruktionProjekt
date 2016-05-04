using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GameEngine.Source.Components
{
    // Position Component in 2D
    public class PositionComponent : IComponent
    {
        // Position
        public Vector2 position;
        public Vector2 prevPosition { get; set; }

        // Constructor for PositionComponent 
        public PositionComponent(Vector2 startPosition)
        {
            position = startPosition;
            prevPosition = position;
        }
    }
}
