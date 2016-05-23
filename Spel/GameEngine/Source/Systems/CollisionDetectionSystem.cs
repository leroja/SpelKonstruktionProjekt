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
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Source.Systems
{
    /// <summary>
    /// a system for detecting collsion between entities
    /// </summary>
    public class CollisionDetectionSystem : IUpdate, IObservable
    {
        private List<IObserver> observers;

        public CollisionDetectionSystem()
        {
            observers = new List<IObserver>();
        }

        public void Notify(IEvent ev)
        {
            foreach (var item in observers)
            {
                item.update(ev);
            }
        }

        public void Subscribe(IObserver observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }
        }

        public void Unsubscribe(IObserver observer)
        {
            if (observers.Contains(observer))
            {
                observers.Remove(observer);
            }
        }

        public void update(GameTime gameTime)
        {
            updatecolRec();

            List<int> dra = ComponentManager.Instance.GetAllEntitiesWithComponentType<CollisionComponent>();
            List<int> done = new List<int>();
            foreach (var item in dra)
            {
                CollisionComponent comp = ComponentManager.Instance.GetEntityComponent<CollisionComponent>(item);
                foreach (var jitem in dra)
                {
                    CollisionComponent comp2 = ComponentManager.Instance.GetEntityComponent<CollisionComponent>(jitem);
                    if (item != jitem)
                    {
                        if (RectangleCollision(item, jitem) && !done.Contains(jitem))
                        {
                            if (comp.isPixelPerfectCompat && comp2.isPixelPerfectCompat)
                            {
                                if (PixelPerfectCollision(item, jitem))
                                {
                                    Notify(new CollisionEvent(item, jitem, gameTime));
                                }
                            }
                            else
                            {
                                Notify(new CollisionEvent(item, jitem, gameTime));
                            }
                        }
                    }
                }
                done.Add(item);
            }
        }


        /// <summary>
        /// Updates the collision rectangles of all collision components
        /// </summary>
        private void updatecolRec()
        {
            List<int> CollisionComp = ComponentManager.Instance.GetAllEntitiesWithComponentType<CollisionComponent>();
            foreach (var item in CollisionComp)
            {
                CollisionRectangleComponent rec = ComponentManager.Instance.GetEntityComponent<CollisionRectangleComponent>(item);
                PositionComponent pos = ComponentManager.Instance.GetEntityComponent<PositionComponent>(item);
                DrawableComponent draw = ComponentManager.Instance.GetEntityComponent<DrawableComponent>(item);
                AnimationComponent ani = ComponentManager.Instance.GetEntityComponent<AnimationComponent>(item);
                if (ani != null && draw != null)
                {
                    rec.CollisionRec = new Rectangle((int)pos.position.X, (int)pos.position.Y, ani.sourceRectangle.Width, ani.sourceRectangle.Height);
                }
                if (draw != null && ani == null)
                {
                    rec.CollisionRec.X = (int)pos.position.X;
                    rec.CollisionRec.Y = (int)pos.position.Y;
                    rec.CollisionRec.Width = draw.texture.Width;
                    rec.CollisionRec.Height = draw.texture.Height;
                }
                else
                {
                    rec.CollisionRec.X = (int)pos.position.X;
                    rec.CollisionRec.Y = (int)pos.position.Y;
                }
            }
        }

        /// <summary>
        /// check if two entities has collided with each other
        /// using bounding rectangle
        /// </summary>
        /// <param name="entity1"> Id of entity 1 </param> 
        /// <param name="entity2"> Id of entity 2 </param>
        /// <returns>
        /// true if bounding rectangle collision occured
        /// and false if no collision or physics is not enabled
        /// </returns>
        private bool RectangleCollision(int entity1, int entity2)
        {
            CollisionRectangleComponent recA = ComponentManager.Instance.GetEntityComponent<CollisionRectangleComponent>(entity1);
            CollisionRectangleComponent recB = ComponentManager.Instance.GetEntityComponent<CollisionRectangleComponent>(entity2);

            if (recA == null || recB == null)
            {
                return false;
            }

            Rectangle rectangleA = recA.CollisionRec;
            Rectangle rectangleB = recB.CollisionRec;

            if (rectangleA.Intersects(rectangleB))
            {
                return true;
            }
            else
                return false;
        }
    
        
        /// <summary>
        /// Checks if two entities has collided with each other
        /// using pixel perfect
        /// </summary>
        /// <param name="entity1"> Id of entity 1 </param>
        /// <param name="entity2"> Id of entity 2 </param>
        /// <returns>
        /// True if pixel perfect collision occurred
        /// and false if no collision or physics is not enabled
        /// </returns>
        private bool PixelPerfectCollision(int entity1, int entity2)
        {
            CollisionRectangleComponent recA = ComponentManager.Instance.GetEntityComponent<CollisionRectangleComponent>(entity1);
            CollisionRectangleComponent recB = ComponentManager.Instance.GetEntityComponent<CollisionRectangleComponent>(entity2);

            Rectangle rectangleA = recA.CollisionRec;
            Rectangle rectangleB = recB.CollisionRec;

            if (!rectangleA.Intersects(rectangleB))
                return false;

            DrawableComponent ent1 = ComponentManager.Instance.GetEntityComponent<DrawableComponent>(entity1);
            DrawableComponent ent2 = ComponentManager.Instance.GetEntityComponent<DrawableComponent>(entity2);

            Color[] dataA = new Color[ent1.texture.Width * ent1.texture.Height];
            ent1.texture.GetData(dataA);
            Color[] dataB = new Color[ent2.texture.Width * ent2.texture.Height];
            ent2.texture.GetData(dataB);

            Rectangle its = Rectangle.Intersect(rectangleA, rectangleB);

            // Check every point within the intersection bounds
            for (int y = its.Top; y < its.Bottom; y++)
            {
                for (int x = its.Left; x < its.Right; x++)
                {
                    // Get the color of both pixels at this point
                    Color colorA = dataA[(x - rectangleA.Left) + (y - rectangleA.Top) * rectangleA.Width];
                    Color colorB = dataB[(x - rectangleB.Left) + (y - rectangleB.Top) * rectangleB.Width];

                    // If both pixels are not completely transparent,
                    if (colorA.A != 0 && colorB.A != 0)
                    {
                        // then an intersection has been found
                        return true;
                    }
                }
            }
            return false;
        }
    }
}