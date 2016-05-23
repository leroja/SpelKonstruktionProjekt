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
    /// a system for drawing the hud of the player
    /// </summary>
    class HUDSystem : IDraw
    {
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


                    if (pc != null && hudc != null && hc != null && ani != null)
                    {
                        int width = hudc.texture.Width;
                        hudc.position.X = pc.position.X + ani.sourceRectangle.Width / 2 - hudc.texture.Width * hc.health /2;
                        hudc.position.Y = pc.position.Y - hudc.texture.Height;
                        for ( int i = 0; i < hc.health; i++)
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
