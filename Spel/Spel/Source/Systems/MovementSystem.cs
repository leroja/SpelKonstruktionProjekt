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
            if(dra != null)
            {
                foreach (var a in dra)
                {
                    PositionComponent p = ComponentManager.Instance.GetEntityComponent<PositionComponent>(a);
                    VelocityComponent v = ComponentManager.Instance.GetEntityComponent<VelocityComponent>(a);
                    KeyBoardComponent kbc = ComponentManager.Instance.GetEntityComponent<KeyBoardComponent>(a);
                    if (p != null && v != null)
                    {
                        //p.position.X += v.velocity.X * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                    if (p != null && v != null && kbc != null)
                    {
                        p.prevPosition = p.position;
                        if (kbc.state[ActionsEnum.Up] == ButtonStates.Hold)
                        {
                            p.position.Y -= v.velocity.Y * (float)gameTime.ElapsedGameTime.TotalSeconds;
                            ComponentManager.Instance.AddComponentToEntity(a, new SoundEffectComponent("Bouncy")); // @temp
                        }
                        //// just for demo @temp
                        if (kbc.state[ActionsEnum.Down] == ButtonStates.Hold)
                        {
                            p.position.Y += v.velocity.Y * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        }
                        if (kbc.state[ActionsEnum.Left] == ButtonStates.Hold)
                        {
                            p.position.X -= v.velocity.X * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        }
                        if (kbc.state[ActionsEnum.Right] == ButtonStates.Hold)
                        {
                            p.position.X += v.velocity.X * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        }
                    }
                }
            }
        }
    }
}
