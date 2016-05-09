using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Source.Components
{
    public class DrawableTextComponent : IComponent
    {
        public string text { get; set; }
        public Color textColor { get; set; }
        public SpriteFont font { get; set; }
        public bool visable { get; set; }



        public DrawableTextComponent(string text, Color color, SpriteFont font)
        {
            this.text = text;
            this.textColor = color;
            this.font = font;
            visable = true;
        }
    }
}
