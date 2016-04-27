using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Source.Systems
{
    interface IDraw
    {

        
        void draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
