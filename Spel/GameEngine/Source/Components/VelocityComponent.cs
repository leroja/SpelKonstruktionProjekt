using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GameEngine.Source.Components
{
    // VelocetyComponent
    class VelocityComponent
    {
        // velocity
        public Vector2 velocity { get; set; }
        // Speed of velocity
        public float speed { get; set; }
        // Direction of velocity.
        public int direction { get; set; }

        // Contstructor for velocityComponent
        public VelocityComponent(Vector2 velocity, float speed)
        {
            this.velocity = velocity;
            this.speed = speed;
        }
    }
}
