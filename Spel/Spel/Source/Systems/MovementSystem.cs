using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Source.Components;
using GameEngine.Source.Managers;
using GameEngine.Source.Systems;
using GameEngine.Source.Systems.Interfaces;
using GameEngine.Source.Enumerator;
using Spel.Source.Components;
using Spel.Source.Enum;

namespace Spel.Source.Systems
{
    /// <summary>
    /// A system for handling movement of players
    /// gravition is also handled in this system
    /// </summary>
    public class MovementSystem : IUpdate
    {
        private const float gravity = 500F;
        private const float sideMovement = 200F;
        public void update(GameTime gameTime)
        {
            List<int> dra = ComponentManager.Instance.GetAllEntitiesWithComponentType<PlayerComponent>();
            List<int> nonPlayerEntities = ComponentManager.Instance.GetAllEntitiesWithComponentType<MovementComponent>();

            if (nonPlayerEntities != null)
            {
                foreach (var entity in nonPlayerEntities)
                {
                    PositionComponent pos = ComponentManager.Instance.GetEntityComponent<PositionComponent>(entity);
                    MovementComponent move = ComponentManager.Instance.GetEntityComponent<MovementComponent>(entity);
                    pos.prevPosition = pos.position;

                    pos.position += move.movement;
                }
            }

            if (dra != null)
            {
                foreach (var a in dra)
                {
                    PositionComponent p = ComponentManager.Instance.GetEntityComponent<PositionComponent>(a);
                    PlayerComponent pc = ComponentManager.Instance.GetEntityComponent<PlayerComponent>(a);
                    VelocityComponent v = ComponentManager.Instance.GetEntityComponent<VelocityComponent>(a);
                    KeyBoardComponent kbc = ComponentManager.Instance.GetEntityComponent<KeyBoardComponent>(a);
                    DirectionComponent dc = ComponentManager.Instance.GetEntityComponent<DirectionComponent>(a);
                    JumpComponent jump = ComponentManager.Instance.GetEntityComponent<JumpComponent>(a);
                    p.prevPosition = p.position;
                    if (dc != null && v != null)
                    {
                        v.velocity.X = sideMovement * (int)dc.directio;
                    }
                    if (p != null && v != null && kbc != null && jump != null && dc != null)
                    {
                        if (kbc.state[ActionsEnum.Jump] == ButtonStates.Pressed && !pc.isFalling)
                        {
                            if(dc.directio == Direction.Still)
                            {
                                dc.directio = dc.preDir;
                            }
                            if (v.velocity.Y > -jump.maxJumpHeight)
                            {
                                v.velocity.Y -= jump.jumpHeight;
                                ComponentManager.Instance.AddComponentToEntity(a, new SoundEffectComponent("jump"));
                            }
                        }
                        v.velocity.Y += gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        p.position += v.velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                }
            }
        }
    }
}
