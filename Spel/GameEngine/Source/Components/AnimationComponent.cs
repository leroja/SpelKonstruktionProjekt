using Microsoft.Xna.Framework;
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
        ///A frame is a single image (or sprite) from the spritesheet. 

        /// the number of frames per second to be drawn
        /// we need to tell our spritesheet how many frames to wait before transitioning.
        public double framesPerSecond { get; set; }
        public int currentFrame { get; set; }

        /// the elapsed time since the last frame increment, and other options.
        public double timeElapsedSinceLastFrame { get; set; }

        ///These variables are used for calculating how many pictures there is in the animation
        ///I.e there is animationNumRows * animationNumColumns number of pictures in the animation.
        ///currentFrame/animationNumColumns can be used to calculate which row we are suposed to be drawing.
        public int numFramesInRow { get; set; }
        public int numFramesInColumn { get; set; }

        public Rectangle sourceRectangle = new Rectangle();

        public AnimationComponent(int animationSizeWidth, int animationSizeHeight, int textureWidth, int textureHeight, double framesPerSecond)
        {
            this.framesPerSecond = framesPerSecond;
            numFramesInColumn = textureWidth / animationSizeWidth;
            numFramesInRow = textureHeight / animationSizeHeight;
            sourceRectangle.Width = animationSizeWidth;
            sourceRectangle.Height = animationSizeHeight;
            this.currentFrame = 0;
        }

        public void setNewPosRectangele(int frame)
        {
            int y = frame / numFramesInColumn;
            int x = frame - (y * numFramesInColumn);
            sourceRectangle.X = x * sourceRectangle.Width;
            sourceRectangle.Y = y * sourceRectangle.Height;
        }

        public int getAnimationLength()
        {
            return numFramesInColumn * numFramesInRow;
        }

       
    }
}
