using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Source.Components
{
    class DrawableTextComponent : Component
    {
        public string text { get; set; }
        public Color textColor { get; set; }
        public SpriteFont font { get; set; }

    }
}
