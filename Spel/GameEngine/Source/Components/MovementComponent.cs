using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;


namespace GameEngine.Source.Components
{
    public class MovementComponent : IComponent
    {
        public Vector2 movement { get; set; }

        public MovementComponent(Vector2 movement)
        {
            this.movement = movement;
        }
    }
}
