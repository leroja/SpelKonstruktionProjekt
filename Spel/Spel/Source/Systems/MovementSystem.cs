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
    public class MovementSystem : IUpdate
    {
        public void update(GameTime gameTime)
        {

            List<int> dra = ComponentManager.Instance.GetAllEntitiesWithComponentType<VelocityComponent>();
            if (dra != null)
            {
                foreach (var a in dra)
                {
                    PositionComponent p = ComponentManager.Instance.GetEntityComponent<PositionComponent>(a);
                    VelocityComponent v = ComponentManager.Instance.GetEntityComponent<VelocityComponent>(a);
                    KeyBoardComponent kbc = ComponentManager.Instance.GetEntityComponent<KeyBoardComponent>(a);
                    DirectionComponent dc = ComponentManager.Instance.GetEntityComponent<DirectionComponent>(a);
                    JumpComponent jump = ComponentManager.Instance.GetEntityComponent<JumpComponent>(a);
                    p.prevPosition = p.position;
                    float gravity = 500;
                    if (dc != null && v != null)
                    {
                        v.velocity.X = 200f * (int)dc.directio;
                    }
                    if (p != null && v != null && kbc != null)
                    {
                        if (kbc.state[ActionsEnum.Up] == ButtonStates.Pressed)
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
                        //// just for demo
                        //if (kbc.state[ActionsEnum.Down] == ButtonStates.Pressed)
                        //{
                        //    v.velocity.Y += (v.speed + 1000f) * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        //}
                        //if (kbc.state[ActionsEnum.Left] == ButtonStates.Pressed)
                        //{
                        //    v.velocity.X -= v.speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        //}
                        //if (kbc.state[ActionsEnum.Right] == ButtonStates.Pressed)
                        //{
                        //    v.velocity.X += v.speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        //}
                        v.velocity.Y += gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        p.position += v.velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

                    }
                }


            }
        }
    }
}
