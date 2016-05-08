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
                    p.prevPosition = p.position;
                    float gravity = 0.5f;
                    if (p != null && v != null)
                    {
                         v.velocity.X += 0.001f;
                    }
                    if (p != null && v != null && kbc != null)
                    {
                        if (kbc.state[ActionsEnum.Up] == ButtonStates.Pressed)
                        {
                            v.velocity.Y = -v.jumpHeight;
                            ComponentManager.Instance.AddComponentToEntity(a, new SoundEffectComponent("jump"));
                        }
                        //// just for demo
                        if (kbc.state[ActionsEnum.Down] == ButtonStates.Pressed)
                        {
                            v.velocity.Y += v.speed + 1000f;
                        }
                        if (kbc.state[ActionsEnum.Left] == ButtonStates.Pressed)
                        {
                            v.velocity.X -= v.speed;
                        }
                        if (kbc.state[ActionsEnum.Right] == ButtonStates.Pressed)
                        {
                            v.velocity.X += v.speed;
                        }
                        v.velocity.Y += gravity;
                        p.position += v.velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

                    }
                }


            }
        }
    }
}
