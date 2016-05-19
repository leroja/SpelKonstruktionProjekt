using GameEngine.Source.Systems.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using GameEngine.Source.Components;
using GameEngine.Source.Managers;
using Spel.Source.Components;
using Spel.Source.Enum;

namespace Spel.Source.Systems
{
    class AISystem : IUpdate
    {
        private const float gravity = 500F;
        private const float sideMovement = 200F;
        public void update(GameTime gameTime)
        {
            Dictionary<int, IComponent> dic = ComponentManager.Instance.GetAllEntitiesAndComponentsWithComponentType<AIComponent>();

            if(dic != null)
            {
                foreach (var item in dic)
                {
                    int id = item.Key;

                    PositionComponent pos = ComponentManager.Instance.GetEntityComponent<PositionComponent>(id);
                    VelocityComponent vel = ComponentManager.Instance.GetEntityComponent<VelocityComponent>(id);
                    DirectionComponent dir = ComponentManager.Instance.GetEntityComponent<DirectionComponent>(id);
                    JumpComponent jump = ComponentManager.Instance.GetEntityComponent<JumpComponent>(id);
                    PlayerComponent pc = ComponentManager.Instance.GetEntityComponent<PlayerComponent>(id);
                    DirectionComponent dc = ComponentManager.Instance.GetEntityComponent<DirectionComponent>(id);

                    
                    if(pos.position.Y + 30 > Game.Instance.GraphicsDevice.Viewport.Height/2 && !pc.isFalling)
                    {
                        if (dc.directio == Direction.Still)
                        {
                            dc.directio = dc.preDir;
                        }
                        if (vel.velocity.Y > -jump.maxJumpHeight)
                        {
                            vel.velocity.Y -= jump.jumpHeight;
                            ComponentManager.Instance.AddComponentToEntity(id, new SoundEffectComponent("jump"));
                        }
                    }

                    vel.velocity.Y += gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    pos.position += vel.velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }

            }
        }
    }
}
