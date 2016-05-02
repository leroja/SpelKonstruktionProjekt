using GameEngine.Source.Components;
using GameEngine.Source.Managers;
using GameEngine.Source.Systems.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Source.Systems
{
    public class TestDrawSystem : IDraw
    {
        // At the top of your class:
        Texture2D pixel;
        GraphicsDeviceManager GameBase;

        public TestDrawSystem(GraphicsDeviceManager afu)
        {
            GameBase = afu;
        }


        public void draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // Somewhere in your LoadContent() method:
            pixel = new Texture2D(GameBase.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            pixel.SetData(new[] { Color.White }); // so that we can draw whatever color we want on top of it


            List<int> ents = ComponentManager.Instance.GetAllEntitiesWithComponentType<CollisionRectangleComponent>();



            spriteBatch.Begin();

            // Create any rectangle you want. Here we'll use the TitleSafeArea for fun.
            //Rectangle titleSafeRectangle = GameBase.GraphicsDevice.Viewport.TitleSafeArea;

            // Call our method (also defined in this blog-post)
            //DrawBorder(titleSafeRectangle, 5, Color.Red, spriteBatch);

            if(ents != null)
            {
                foreach (var ent in ents)
                {
                    CollisionRectangleComponent rec =  ComponentManager.Instance.GetEntityComponent<CollisionRectangleComponent>(ent);
                    DrawBorder(rec.CollisionRec, 2, Color.DarkRed, spriteBatch);
                }
            }

            spriteBatch.End();
        }

        /// <summary>
        /// Will draw a border (hollow rectangle) of the given 'thicknessOfBorder' (in pixels)
        /// of the specified color.
        ///
        /// By Sean Colombo, from http://bluelinegamestudios.com/blog
        /// </summary>
        /// <param name="rectangleToDraw"></param>
        /// <param name="thicknessOfBorder"></param>
        private void DrawBorder(Rectangle rectangleToDraw, int thicknessOfBorder, Color borderColor, SpriteBatch spriteBatch)
        {
            // Draw top line
            spriteBatch.Draw(pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, rectangleToDraw.Width, thicknessOfBorder), borderColor);

            // Draw left line
            spriteBatch.Draw(pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, thicknessOfBorder, rectangleToDraw.Height), borderColor);

            // Draw right line
            spriteBatch.Draw(pixel, new Rectangle((rectangleToDraw.X + rectangleToDraw.Width - thicknessOfBorder),
                                            rectangleToDraw.Y,
                                            thicknessOfBorder,
                                            rectangleToDraw.Height), borderColor);
            // Draw bottom line
            spriteBatch.Draw(pixel, new Rectangle(rectangleToDraw.X,
                                            rectangleToDraw.Y + rectangleToDraw.Height - thicknessOfBorder,
                                            rectangleToDraw.Width,
                                            thicknessOfBorder), borderColor);
        }
    }
}
