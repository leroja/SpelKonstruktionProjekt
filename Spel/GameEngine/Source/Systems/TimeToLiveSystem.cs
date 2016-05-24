using GameEngine.Source.Systems.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using GameEngine.Source.Components;
using GameEngine.Source.Managers;

namespace GameEngine.Source.Systems
{
    public class TimeToLiveSystem : IUpdate
    {
        public void update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Dictionary<int, IComponent> dic = ComponentManager.Instance.GetAllEntitiesAndComponentsWithComponentType<TTLComponent>();
            if(dic != null)
            {
                foreach (var item in dic)
                {
                    TTLComponent ttl = (TTLComponent)item.Value;
                    ttl.curTime += dt;
                }
            }

        }
    }
}
