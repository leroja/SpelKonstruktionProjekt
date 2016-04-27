using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Source.Components
{
    /// <summary>
    /// AnimationComponent, is added to entities which contains some kind of animation
    /// </summary>
    public class AnimationComponent : IComponent
    {
        ///We need to add something more here, when we know what information we need for the animations
        public bool visable { get; set; }
        /// <summary>
        /// AnimationComponent constructor
        /// </summary>
        public AnimationComponent()
        {
            visable = true;
        }
    }
}
