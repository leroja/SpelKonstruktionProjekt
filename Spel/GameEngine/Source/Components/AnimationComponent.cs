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
        ///"a list of frames (which are image components)" <-needs to be added here
        
        public bool visable { get; set; }
        /// the number of frames per second to be drawn
        public int framesPerSecond { get; set; }
        public int currentFrame { get; set; }

        /// the elapsed time since the last frame increment, and other options.
        public double timeElapsedSinceLastFrame { get; set; }
        
        /// <summary>
        /// AnimationComponent constructor
        /// </summary>
        public AnimationComponent()
        {
            visable = true;
        }
    }
}
