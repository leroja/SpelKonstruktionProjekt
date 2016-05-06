using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using GameEngine.Source.Components;
using GameEngine.Source.Managers;
using GameEngine.Source.Systems.Interfaces;
using GameEngine.Source.RandomStuff;
using GameEngine.Source.Enumerator;

namespace GameEngine.Source.Systems
{
    public class CollisionDetectionSystem : IUpdate
    {
        public void update(GameTime gameTime)
        {
            List<Collision> collisions = new List<Collision>();
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
                                    collisions.Add(new Collision(item, jitem));
                                }
                            }
                            else
                            {
                                collisions.Add(new Collision(item, jitem));
                            }
                        }
                    }
                }
            }

            if(collisions.Count > 0)
            {
                CollisionHappenedComponent cpc = new CollisionHappenedComponent();
                cpc.collisions = collisions;
                int id =  ComponentManager.Instance.CreateID();
                ComponentManager.Instance.AddComponentToEntity(id, cpc);
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
