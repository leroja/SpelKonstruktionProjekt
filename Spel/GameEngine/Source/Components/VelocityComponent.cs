using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GameEngine.Source.Components
{
    // VelocetyComponent
    public class VelocityComponent : IComponent
    {
        // velocity
        public Vector2 velocity;
        // Speed of velocity
        public float speed { get; set; }
        // Direction of velocity.
        public int direction { get; set; }
        public float jumpHeight { get; set; }

        // Contstructor for velocityComponent
        public VelocityComponent(Vector2 velocity, float speed, float jh)
        {
            this.velocity = velocity;
            this.speed = speed;
            this.jumpHeight = jh;
        }
    }
}
