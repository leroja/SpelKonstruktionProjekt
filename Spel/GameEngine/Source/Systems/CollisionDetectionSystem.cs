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
            updatecolRec();
            List<int> dra = ComponentManager.Instance.GetAllEntitiesWithComponentType<CollisionComponent>();
            foreach (var item in dra)
            {
                CollisionComponent comp = ComponentManager.Instance.GetEntityComponent<CollisionComponent>(item);
                foreach (var jitem in dra)
                {
                    CollisionComponent comp2 = ComponentManager.Instance.GetEntityComponent<CollisionComponent>(jitem);
                    if (item != jitem)
                    {
                        if(PhysicsManager.Instance.RectangleCollision(item, jitem))
                        {
                            if (comp.isPixelPerfectCompat && comp2.isPixelPerfectCompat)
                            {
                                if (PhysicsManager.Instance.PixelPerfectCollision(item, jitem))
                                {
                                    //Console.Out.WriteLine("BAM2!!between" + item + " " + jitem);
                                }
                            }
                            else
                            {
                                //test
                                //Console.Out.WriteLine("BAM1!!between" + item + " " + jitem);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Updates the collision rectangles of all collision components
        /// </summary>
        private void updatecolRec()
        {
            List<int> CollisionComp = ComponentManager.Instance.GetAllEntitiesWithComponentType<CollisionComponent>();

            foreach(var item in CollisionComp)
            {
                CollisionRectangleComponent rec = ComponentManager.Instance.GetEntityComponent<CollisionRectangleComponent>(item);
                PositionComponent pos = ComponentManager.Instance.GetEntityComponent<PositionComponent>(item);
                DrawableComponent draw = ComponentManager.Instance.GetEntityComponent<DrawableComponent>(item);
                if (draw != null)
                {
                    rec.CollisionRec = new Rectangle((int)pos.position.X, (int)pos.position.Y, draw.texture.Width, draw.texture.Height);
                }
                else
                {
                    rec.CollisionRec = new Rectangle((int)pos.position.X, (int)pos.position.Y, rec.CollisionRec.Width, rec.CollisionRec.Height);
                }
            }
        }
    }
}
