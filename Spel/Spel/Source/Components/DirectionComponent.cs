using GameEngine.Source.Components;
using Spel.Source.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// a component for movement direction on the X-axis
/// </summary>
namespace Spel.Source.Components
{
    public class DirectionComponent :IComponent
    {
        public Direction directio { get; set; }
        public Direction preDir { get; set; }

        public DirectionComponent(Direction dir)
        {
            directio = dir;
        }
    }
}
