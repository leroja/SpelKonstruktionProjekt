using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Source.Managers
{
    /// <summary>
    /// The RenderManager class is used to manage the size of the game window
    /// </summary>
    class RenderManager
    {
        private GraphicsDeviceManager graphics;
        /// <summary>
        /// The constructor for the RenderManager takes a grapgicsdevicemanager as parameter, which it then uses to manipulate.
        /// graphical elements in the game window.
        /// </summary>
        /// <param name="graphics"></param>
        /// The GraphicsDeviceManager that will be used by the RenderManager.
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
        public void applyFullScreen()
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

        /// <summary>
        /// This function loads all files in the content pipeline, and returns a list containing all of these.
        /// </summary>
        /// <param name="sprites">
        /// The content pipeline.
        /// </param>
        /// <param name="files">
        /// The names of the files in the content pipeline.
        /// </param>
        /// <returns>
        /// The list of loaded content.
        /// </returns>
        public List<Texture2D> loadContent(ContentManager sprites, List<string> files)
        {
            List<Texture2D> list = new List<Texture2D>();

            foreach(string sprite in files){
                list.Add(sprites.Load<Texture2D>(sprite));
            }
            return list;
        }
    }
}
