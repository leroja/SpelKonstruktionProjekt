using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Source.Components;
using GameEngine.Source.Managers;
using GameEngine.Source.Systems;

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
                if (p != null && v != null)
                {
                    p.position.X += v.speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            }

        }
        }
}
