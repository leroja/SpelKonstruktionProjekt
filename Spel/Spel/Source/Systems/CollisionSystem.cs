using GameEngine.Source.Systems.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using GameEngine.Source.Managers;
using GameEngine.Source.Components;

namespace Spel.Source.Systems
{
    class CollisionSystem : IUpdate
    {
        public void update(GameTime gameTime)
        {
            List<int> ents = ComponentManager.Instance.GetAllEntitiesWithComponentType<CollisionHappenedComponent>();

            if(ents != null)
            {
                foreach (var ent in ents)
                {
                    CollisionHappenedComponent c = ComponentManager.Instance.GetEntityComponent<CollisionHappenedComponent>(ent);

                    if(c.collisions != null)
                    {
                        foreach (var coll in c.collisions)
                        {
                            int ent1 = coll.entity1;
                            int ent2 = coll.entity2;

                            VelocityComponent vel1 = ComponentManager.Instance.GetEntityComponent<VelocityComponent>(ent1);
                            VelocityComponent vel2 = ComponentManager.Instance.GetEntityComponent<VelocityComponent>(ent2);

                            if (vel1 != null)
                            {
                                PositionComponent pos = ComponentManager.Instance.GetEntityComponent<PositionComponent>(ent1);
                                if(pos != null)
                                {
                                    pos.position = pos.prevPosition;
                                }
                            }
                            else if (vel2 != null)
                            {
                                PositionComponent pos = ComponentManager.Instance.GetEntityComponent<PositionComponent>(ent2);
                                if (pos != null)
                                {
                                    pos.position = pos.prevPosition;
                                }
                            }
                            
                            

                        }

                    }

                    ComponentManager.Instance.RecycleID(ent);
                    ComponentManager.Instance.RemoveEntity(ent);
                }
            }
        }
    }
}
