﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Source.Components
{
    // DrawableComponent For Drawing 2D textures
    class DrawableComponent : Component
    {
        // 2D texture For Drawing component
        public Texture2D texture { get; set; }
        public SpriteEffects sprite { get; set; }

        // Constructor for DrawableComponent 
        public DrawableComponent(Texture2D startingTexture)
        {
            texture = startingTexture;
            sprite = SpriteEffects.None;
        }
    }
}
