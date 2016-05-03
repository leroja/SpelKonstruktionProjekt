using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameEngine.Source.Managers;
using GameEngine.Source.Components;
using GameEngine.Source.Systems.Interfaces;

namespace GameEngine.Source.Systems
{
    /// <summary>
    /// A system that draws 2D sprites
    /// </summary>
    public class _2DSpriteSystem : IDraw
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public void draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            List<int> dra = ComponentManager.Instance.GetAllEntitiesWithComponentType<DrawableComponent>();
            if (dra != null)
            {
                spriteBatch.Begin();
                foreach (var a in dra)
                {
                    PositionComponent p = ComponentManager.Instance.GetEntityComponent<PositionComponent>(a);
                    DrawableComponent d = ComponentManager.Instance.GetEntityComponent<DrawableComponent>(a);

                    if (p != null && d != null)
                    {
                        spriteBatch.Draw(d.texture, p.position, Color.White);
                    }
                }
                spriteBatch.End();
            }
        }
    }
}
