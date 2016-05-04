using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameEngine.Source.Components
{
    // DrawableComponent For Drawing 2D textures
    public class DrawableComponent : IComponent
    {
        // 2D texture For Drawing component
        public Texture2D texture { get; set; }
        public SpriteEffects sprite { get; set; }
        public Rectangle drawRectangle { get; set; }

        // Constructor for DrawableComponent 
        public DrawableComponent(Texture2D startingTexture)
        {
            texture = startingTexture;
            sprite = SpriteEffects.None;
            drawRectangle = new Rectangle(0, 0, startingTexture.Width, startingTexture.Height);
        }

    }
}
