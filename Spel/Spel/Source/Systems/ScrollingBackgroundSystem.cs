using GameEngine.Source.Systems.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Spel.Source.Systems
{
    class ScrollingBackgroundSystem : IDraw, IUpdate
    {
        public bool active { get; set; }
        private Texture2D texture;
        private int height;
        private int width;
        public Rectangle rec1, rec2;
        private double time = 12;

        public ScrollingBackgroundSystem(GraphicsDevice device, Texture2D newTexture)
        {
            active = false;
            texture = newTexture;
            height = device.Viewport.Height;
            width = device.Viewport.Width;
            rec1 = new Rectangle(0, 0, width, height);
            rec2 = new Rectangle(width, 0, width, height);

        }
        public void draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (active)
            {
                spriteBatch.Draw(texture, rec1, Color.White);
                spriteBatch.Draw(texture, rec2, Color.White);
            }
        }

        public void update(GameTime gameTime)
        {
            if (active)
            {
                time = time - gameTime.ElapsedGameTime.TotalMilliseconds;
                if (time < 0)
                {
                    if (rec1.X + texture.Width <= 0)
                        rec1.X = rec2.X + texture.Width;
                    if (rec2.X + texture.Width <= 0)
                        rec2.X = rec1.X + texture.Width;
                    rec1.X -= 5;
                    rec2.X -= 5;
                    time = 12;
                }
            }
        }
    }
}
