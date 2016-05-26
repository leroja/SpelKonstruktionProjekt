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

namespace Spel.Source.Systems
{
    /// <summary>
    /// A system for handling the health of entities
    /// </summary>
    public class HealthSystem : IUpdate
    {
        public List<int> deathList;

        public void initialize()
        {
            deathList = new List<int>();
        }
        public void update(GameTime gameTime)
        {
            List<int> entitys = ComponentManager.Instance.GetAllEntitiesWithComponentType<HealthComponent>();
            
            if (entitys != null)
            {
                if (deathList.Count == 0)
                {
                    foreach (var en in entitys)
                    {
                        deathList.Add(en);
                    }
                }

                foreach (var entity in entitys)
                {
                    HealthComponent hc = ComponentManager.Instance.GetEntityComponent<HealthComponent>(entity);
                    if (hc.health <= 0 && !hc.isDead)
                    {
                        CollisionComponent cc = ComponentManager.Instance.GetEntityComponent<CollisionComponent>(entity);
                        ComponentManager.Instance.RemoveComponentFromEntity(entity, cc);
                        VelocityComponent vc = ComponentManager.Instance.GetEntityComponent<VelocityComponent>(entity);
                        ComponentManager.Instance.RemoveComponentFromEntity(entity, vc);
                        hc.isDead = true;
                        if (deathList.Count != 1)
                            deathList.Remove(entity);       

                    }
                }
            }
        }
        public List<int> getLivingPlayers()
        {
            return deathList;
        }
    }
    
}
