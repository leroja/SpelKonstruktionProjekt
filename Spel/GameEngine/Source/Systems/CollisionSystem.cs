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
    public class CollisionSystem : IUpdate
    {
        public void update(GameTime gameTime)
        {
            List<int> dra = ComponentManager.Instance.GetAllEntitiesWithComponentType<CollisionComponent>();
        }
    }
}
