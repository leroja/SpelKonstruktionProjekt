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
    class TextRenderSystem : IDraw
    {
        public void draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            List<int> dra = ComponentManager.Instance.GetAllEntitiesWithComponentType<DrawableTextComponent>();
            if (dra != null)
            {
                spriteBatch.Begin();
                foreach (var a in dra)
                {
                    PositionComponent p = ComponentManager.Instance.GetEntityComponent<PositionComponent>(a);
                    DrawableTextComponent d = ComponentManager.Instance.GetEntityComponent<DrawableTextComponent>(a);

                    if (p != null && d != null)
                    {
                        spriteBatch.DrawString(d.font, d.text, p.position, d.textColor);
                    }
                }
                spriteBatch.End();
            }
        }
    }
}
