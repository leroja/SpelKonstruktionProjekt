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
    /// <summary>
    /// A system that draws the collsion rectangles of all collision rectangle components
    /// </summary>
    public class TestDrawSystem : IDraw
    {
        Texture2D pixel;
        GraphicsDeviceManager GameBase;

        public TestDrawSystem(GraphicsDeviceManager afu)
        {
            GameBase = afu;

            pixel = new Texture2D(GameBase.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            pixel.SetData(new[] { Color.White });
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public void draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            List<int> ents = ComponentManager.Instance.GetAllEntitiesWithComponentType<CollisionRectangleComponent>();

            

            if(ents != null)
            {
                foreach (var ent in ents)
                {
                    
                    CollisionRectangleComponent rec =  ComponentManager.Instance.GetEntityComponent<CollisionRectangleComponent>(ent);
                    DrawBorder(rec.CollisionRec, 2, Color.DarkRed, spriteBatch);

                    DrawableComponent dc = ComponentManager.Instance.GetEntityComponent<DrawableComponent>(ent);
                    if (dc != null)
                    {
                        Rectangle rc = new Rectangle(rec.CollisionRec.X, rec.CollisionRec.Y, dc.texture.Width, dc.texture.Height);

                        DrawBorder(rc, 2, Color.YellowGreen, spriteBatch);
                    }
                }
            }
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
