using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Source.Components
{
    public class JumpComponent : IComponent
    {
        public float jumpHeight { get; set; }
        public float maxJumpHeight { get; set; }

        public JumpComponent(float jumpHeight, float maxJumpHeight)
        {
            this.jumpHeight = jumpHeight;
            this.maxJumpHeight = maxJumpHeight;
        }
    }
}
