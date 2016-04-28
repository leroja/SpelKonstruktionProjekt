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

namespace Spel.Source.Systems
{
    public class MovementSystem : IUpdate
    {
        public void update(GameTime gameTime)
        {
            List<int> dra = ComponentManager.Instance.GetAllEntitiesWithComponentType<VelocityComponent>();
            foreach (var a in dra)
            {
                PositionComponent p = ComponentManager.Instance.GetEntityComponent<PositionComponent>(a);
                VelocityComponent v = ComponentManager.Instance.GetEntityComponent<VelocityComponent>(a);
                KeyBoardComponent kbc = ComponentManager.Instance.GetEntityComponent<KeyBoardComponent>(a);
                if (p != null && v != null)
                {
                    p.position.X += v.speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                if(p != null && v != null && kbc != null)
                {
                    string testb = kbc.state["Up"];
                    if (kbc.state["Up"].Equals("Pressed"))
                    {
                        p.position.Y -= v.jumpHeight * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                    //// just for demo
                    if (kbc.state["Down"].Equals("Pressed"))
                    {
                        p.position.Y += v.jumpHeight * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                    if (kbc.state["Left"].Equals("Pressed"))
                    {
                        p.position.X -= v.jumpHeight * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                    if (kbc.state["Right"].Equals("Pressed"))
                    {
                        p.position.X += v.jumpHeight * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                }

            }

        }
        }
}
