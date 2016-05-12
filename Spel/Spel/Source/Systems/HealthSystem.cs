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
    /// a system for managing the health of entities
    /// </summary>
    public class HealthSystem : IUpdate
    {
        public void update(GameTime gameTime)
        {
            //@Todo this system is not done yeti

            Dictionary<int, IComponent> dic = ComponentManager.Instance.GetAllEntitiesAndComponentsWithComponentType<HealthComponent>();

            if (dic != null)
            {
                foreach (var item in dic)
                {
                    HealthComponent hc = (HealthComponent)item.Value;

                    if (hc.health > hc.maxhealth)
                    {
                        hc.health = hc.maxhealth;
                    }

                    if (hc.health <= 0)
                    {
                        //Console.WriteLine("death");
                        // death?
                    }
                    else if (hc.health == 1)
                    {
                    }
                    else if (hc.health == 2)
                    {
                    }
                    else if (hc.health == 3)
                    {
                    }else
                    {

                    }
                }
            }
        }
    }
}
