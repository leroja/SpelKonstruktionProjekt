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
using Spel.Source.Components;

namespace Spel.Source.Systems
{
    /// <summary>
    /// a system for drawing the hud of the player/AI
    /// </summary>
    class HUDSystem : IDraw
    {
        private int width;
        public void draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            List<int> entitys = ComponentManager.Instance.GetAllEntitiesWithComponentType<HUDComponent>();
            
            if (entitys != null)
            {
                foreach (var entity in entitys)
                {
                    PositionComponent pc = ComponentManager.Instance.GetEntityComponent<PositionComponent>(entity);
                    HUDComponent hudc  = ComponentManager.Instance.GetEntityComponent<HUDComponent>(entity);
                    DrawableComponent dc = ComponentManager.Instance.GetEntityComponent<DrawableComponent>(entity);
                    HealthComponent hc = ComponentManager.Instance.GetEntityComponent<HealthComponent>(entity);
                    AnimationComponent ani = ComponentManager.Instance.GetEntityComponent<AnimationComponent>(entity);
                    width = hudc.texture.Width;

                    if (pc != null && hudc != null && hc != null && ani != null)
                    {
                        hudc.position.X = pc.position.X + (ani.sourceRectangle.Width * 0.5f) - (hudc.texture.Width * (hc.health * 0.5f));
                        hudc.position.Y = pc.position.Y - hudc.texture.Height;
                        for ( int i = 0; i < hc.health; i++)
                        {
                            spriteBatch.Draw(hudc.texture, hudc.position, Color.White);
                            hudc.position.X += width;
                        } 
                    }
                    else
                    {
                        hudc.position.X = pc.position.X + (dc.texture.Width * 0.5f) - (hudc.texture.Width * (hc.health * 0.5f));
                        hudc.position.Y = pc.position.Y - hudc.texture.Height;
                        for (int i = 0; i < hc.health; i++)
                        {
                            spriteBatch.Draw(hudc.texture, hudc.position, Color.White);
                            hudc.position.X += width;
                        }
                    }
                }
            }
        }


    }
}
