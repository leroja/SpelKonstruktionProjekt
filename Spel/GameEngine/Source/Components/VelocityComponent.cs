using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GameEngine.Source.Components
{
    class VelocityComponent
    {
        public Vector2 velocity { get; set; }
        public float speed { get; set; }
        public int direction { get; set; }

        public VelocityComponent(Vector2 velocity, float speed)
        {
            this.velocity = velocity;
            this.speed = speed;
        }
    }
}
