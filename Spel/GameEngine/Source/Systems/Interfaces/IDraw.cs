using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Source.Systems
{
    public interface IDraw : ISystem
    {

        
        void draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
