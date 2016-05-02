using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using GameEngine.Source.Components;
using GameEngine.Source.Managers;
using GameEngine.Source.Systems.Interfaces;

namespace GameEngine.Source.Systems
{
    public class CollisionDetectionSystem : IUpdate
    {
        public void update(GameTime gameTime)
        {
            List<int> dra = ComponentManager.Instance.GetAllEntitiesWithComponentType<CollisionComponent>();

            foreach (var item in dra)
            {
                
                foreach (var jitem in dra)
                {
                    if(item != jitem)
                    {
                        if(PhysicsManager.Instance.RectangleCollision(item, jitem))
                        {
                            if(PhysicsManager.Instance.PixelPerfectCollision(item, jitem))
                            {
                                
                            }
                        }
                    }
                }
            }
        }
        private void updatecolRec(int entity)
        {


        }
    }
}
