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
        public double timePerFrame { get; set; }
        public int currentFrame { get; set; }
        public bool oneTime { get; set; }

        /// the elapsed time since the last frame increment, and other options.
        public double timeElapsedSinceLastFrame { get; set; }

        ///These variables are used for calculating how many pictures there is in the animation
        ///I.e there is animationNumRows * animationNumColumns number of pictures in the animation.
        ///currentFrame/animationNumColumns can be used to calculate which row we are suposed to be drawing.
        public int numFramesInRow { get; set; }
        public int numFramesInColumn { get; set; }
        public int numberOfFrames { get; set; }

        public Rectangle sourceRectangle = new Rectangle();

        /// <summary>
        /// AnimationComponent constructor
        /// </summary>
        /// <param name="animationSizeWidth">is an int which is the, desired width of one frame in the animation</param>
        /// <param name="animationSizeHeight">is an int which is the desired height of one frame in the animation</param>
        /// <param name="textureWidth">is an int which is the total with of the 2D texture</param>
        /// <param name="textureHeight">is an int which is the total height of the 2D texture</param>
        /// <param name="timePerFrame">is a double which is the time that should be passed before changing the frame.</param>
        public AnimationComponent(int animationSizeWidth, int animationSizeHeight, int textureWidth, int textureHeight, double timePerFrame)
        {
            this.timePerFrame = timePerFrame;
            numFramesInColumn = textureWidth / animationSizeWidth;
            numFramesInRow = textureHeight / animationSizeHeight;
            sourceRectangle.Width = animationSizeWidth;
            sourceRectangle.Height = animationSizeHeight;
            this.currentFrame = 0;
            numberOfFrames = numFramesInColumn * numFramesInRow - 1;
        }

        /// <summary>
        /// AnimationComponent constructor, an alternative if the texture does not contain a full set of frames in the last row.
        /// Then you can create the animation by defining how many frames the texture contains.
        /// </summary>
        /// <param name="animationSizeWidth">is an int which is the desired width of one frame in the animation</param>
        /// <param name="animationSizeHeight">is an int which is the desired height of one frame in the animation</param>
        /// <param name="textureWidth">is an int which is the total with of the 2D texture</param>
        /// <param name="textureHeight">is an int which is the total height of the 2D texture</param>
        /// <param name="timePerFrame">is a double which is the time that should be passed before changing the frame.</param>
        /// <param name="numberOfFrames">is an int whicch is the amount of frames in the texture</param>
        public AnimationComponent(int animationSizeWidth, int animationSizeHeight, int textureWidth, int textureHeight, double timePerFrame, int numberOfFrames)
        {
            this.timePerFrame = timePerFrame;
            numFramesInColumn = textureWidth / animationSizeWidth;
            numFramesInRow = textureHeight / animationSizeHeight;
            sourceRectangle.Width = animationSizeWidth;
            sourceRectangle.Height = animationSizeHeight;
            this.currentFrame = 0;
            this.numberOfFrames = numberOfFrames;
        }

        /// <summary>
        /// setNewPosRectangle function is used to set a new position for the animation rectangle in order to change frame.
        /// </summary>
        /// <param name="frame">is a int variable which sould be representing the current frame that we're at.</param>
        public void setNewPosRectangle(int frame)
        {
            int y = frame / numFramesInColumn;
            int x = frame - (y * numFramesInColumn);
            sourceRectangle.X = x * sourceRectangle.Width;
            sourceRectangle.Y = y * sourceRectangle.Height;
        }

        /// <summary>
        /// getAnimationLength method returns the length of the animation, this is calculated by the animationFramesInRow * animationFramesInColumn.
        /// </summary>
        /// <returns>returns an int which is the number of frames in the animation.</returns>
        public int getAnimationLength()
        {
            return numberOfFrames;
        }

       
    }
}
