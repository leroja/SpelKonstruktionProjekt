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
    /// a system that draws text
    /// </summary>
    class TextRenderSystem : IDraw
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public void draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            List<int> dra = ComponentManager.Instance.GetAllEntitiesWithComponentType<DrawableTextComponent>();
            if (dra != null)
            {
                spriteBatch.Begin();
                foreach (var a in dra)
                {
                    
                    DrawableTextComponent d = ComponentManager.Instance.GetEntityComponent<DrawableTextComponent>(a);

                    Dictionary<Vector2, string> list = d.getMenuList();
                    if(list != null)
                    {
                        
                        foreach(var item in list)
                        {
                            spriteBatch.DrawString(d.font, item.Value, item.Key, d.textColor);
                        }
                        
                    }
                    else
                    {
                        PositionComponent p = ComponentManager.Instance.GetEntityComponent<PositionComponent>(a);
                        if (p != null && d != null && d.visable == true)
                        {
                            spriteBatch.DrawString(d.font, d.text, p.position, d.textColor);
                        }
                    }
                    
                }
                spriteBatch.End();
            }
        }
    }
}
