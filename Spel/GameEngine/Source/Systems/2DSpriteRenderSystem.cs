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
                foreach (var a in dra)
                {
                    PositionComponent p = ComponentManager.Instance.GetEntityComponent<PositionComponent>(a);
                    DrawableComponent d = ComponentManager.Instance.GetEntityComponent<DrawableComponent>(a);
                    AnimationComponent anim = ComponentManager.Instance.GetEntityComponent<AnimationComponent>(a);

                    if (p != null && d != null && d.visable == true)
                    {
                        if (anim != null)
                        {
                            d.drawRectangle = new Rectangle((int)p.position.X, (int)p.position.Y, anim.sourceRectangle.Width, anim.sourceRectangle.Height);
                            spriteBatch.Draw(d.texture, p.position,anim.sourceRectangle, d.colour, 0, Vector2.Zero, 1, d.flip, 1);
                        }
                        else
                            spriteBatch.Draw(d.texture, p.position,null, d.colour,0,Vector2.Zero,1,d.flip,1);
                    }
                }
            }
        }
    }
}
