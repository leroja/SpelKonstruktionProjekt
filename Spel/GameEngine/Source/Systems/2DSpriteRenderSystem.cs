using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameEngine.Source.Managers;
using GameEngine.Source.Components;

namespace GameEngine.Source.Systems
{
    public class _2DSpriteSystem : IDraw
    {

        public void draw(GameTime gameTime, SpriteBatch spriteBatch)
        {




            List<int> dra = ComponentManager.Instance.GetAllEntitiesWithComponentType<DrawableComponent>();
            spriteBatch.Begin();
            foreach (var a in dra)
            {
                PositionComponent p = ComponentManager.Instance.GetEntityComponent<PositionComponent>(a);
                DrawableComponent d = ComponentManager.Instance.GetEntityComponent<DrawableComponent>(a);

                if(p != null && d != null)
                {
                    spriteBatch.Draw(d.texture, p.position, Color.White);
                }
            }
            spriteBatch.End();
        }
    }
}
