using GameEngine.Source.Managers;
using GameEngine.Source.Systems.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameEngine.Source.Components;

namespace GameEngine.Source.Systems
{
    class HUDSystem : IDraw
    {
        public void draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            List<int> entitys = ComponentManager.Instance.GetAllEntitiesWithComponentType<HUDComponent>();
            if (entitys != null)
            {
                spriteBatch.Begin();
                foreach (var entity in entitys)
                {
                    PositionComponent pc = ComponentManager.Instance.GetEntityComponent<PositionComponent>(entity);
                    HUDComponent hudc  = ComponentManager.Instance.GetEntityComponent<HUDComponent>(entity);
                    DrawableComponent dc = ComponentManager.Instance.GetEntityComponent<DrawableComponent>(entity);
                    HealthComponent hc = ComponentManager.Instance.GetEntityComponent<HealthComponent>(entity);

                    if (pc != null && hudc != null && hc != null)
                    {
                        int width = hudc.texture.Width;
                        Vector2 newposition = pc.position;
                        newposition.Y = pc.position.Y - hudc.texture.Height;
                        for ( int i = 0; i < hc.health; i++)
                        {
                            spriteBatch.Draw(hudc.texture, newposition, Color.White);
                            newposition.X += width;
                        }   
                    }
                }
                spriteBatch.End();
            }
        }


    }
}
