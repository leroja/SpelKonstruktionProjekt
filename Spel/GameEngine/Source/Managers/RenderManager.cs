using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Source.Managers
{
    /// <summary>
    /// The RenderManager class is used to manage the size of the game window, and to render/draw objects onto the screen.
    /// </summary>
    class RenderManager
    {
        private GraphicsDeviceManager graphics;
        /// <summary>
        /// The constructor for the RenderManager takes a grapgicsdevicemanager as parameter, which it then uses to manipulate
        /// graphical elements in the game window.
        /// </summary>
        /// <param name="graphics"></param>
        public RenderManager(GraphicsDeviceManager graphics)
        {
            this.graphics = graphics;
            initialize();
        }
        /// <summary>
        /// Sets the graphics settings to the default values of this game engine.
        /// </summary>
        public void initialize()
        {
            graphics.CreateDevice();
            graphics.PreferredBackBufferWidth = graphics.GraphicsDevice.DisplayMode.Width;
            graphics.PreferredBackBufferHeight = graphics.GraphicsDevice.DisplayMode.Height;
            graphics.ApplyChanges();
        }
        /// <summary>
        /// This function puts the game into full screen mode.
        /// </summary>
        public void activateFullScreen()
        {
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();
        }
        /// <summary>
        /// This function puts the game window, in windowed mode.
        /// </summary>
        public void disableFullScreen()
        {
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
        }


    }
}
