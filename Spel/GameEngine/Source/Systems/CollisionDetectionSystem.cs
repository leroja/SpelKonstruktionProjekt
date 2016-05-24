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
            if (dra != null)
            {
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
        }


        /// <summary>
        /// Updates the collision rectangles of all collision components
        /// </summary>
        private void updatecolRec()
        {
            List<int> CollisionComp = ComponentManager.Instance.GetAllEntitiesWithComponentType<CollisionComponent>();
            if (CollisionComp != null)
            {
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
        //private bool PixelPerfectCollision(int entity1, int entity2)
        //{
        //    CollisionRectangleComponent recA = ComponentManager.Instance.GetEntityComponent<CollisionRectangleComponent>(entity1);
        //    CollisionRectangleComponent recB = ComponentManager.Instance.GetEntityComponent<CollisionRectangleComponent>(entity2);

        //    Rectangle rectangleA = recA.CollisionRec;
        //    Rectangle rectangleB = recB.CollisionRec;

        //    if (!rectangleA.Intersects(rectangleB))
        //        return false;

        //    DrawableComponent ent1 = ComponentManager.Instance.GetEntityComponent<DrawableComponent>(entity1);
        //    DrawableComponent ent2 = ComponentManager.Instance.GetEntityComponent<DrawableComponent>(entity2);

        //    Color[] dataA = new Color[ent1.texture.Width * ent1.texture.Height];
        //    ent1.texture.GetData(dataA);
        //    Color[] dataB = new Color[ent2.texture.Width * ent2.texture.Height];
        //    ent2.texture.GetData(dataB);

        //    Rectangle its = Rectangle.Intersect(rectangleA, rectangleB);

        //    // Check every point within the intersection bounds
        //    for (int y = its.Top; y < its.Bottom; y++)
        //    {
        //        for (int x = its.Left; x < its.Right; x++)
        //        {
        //            // Get the color of both pixels at this point
        //            Color colorA = dataA[(x - rectangleA.Left) + (y - rectangleA.Top) * rectangleA.Width];
        //            Color colorB = dataB[(x - rectangleB.Left) + (y - rectangleB.Top) * rectangleB.Width];

        //            // If both pixels are not completely transparent,
        //            if (colorA.A != 0 && colorB.A != 0)
        //            {
        //                // then an intersection has been found
        //                return true;
        //            }
        //        }
        //    }
        //    return false;
        //}

        private bool PixelPerfectCollision(int entity2, int entity1)
        {
            Rectangle Source1;
            Rectangle Source2;




            CollisionRectangleComponent recA = ComponentManager.Instance.GetEntityComponent<CollisionRectangleComponent>(entity1);
            CollisionRectangleComponent recB = ComponentManager.Instance.GetEntityComponent<CollisionRectangleComponent>(entity2);

            Rectangle rectangleA = recA.CollisionRec;
            Rectangle rectangleB = recB.CollisionRec;

            if (!rectangleA.Intersects(rectangleB))
                return false;

            DrawableComponent ent1 = ComponentManager.Instance.GetEntityComponent<DrawableComponent>(entity1);
            DrawableComponent ent2 = ComponentManager.Instance.GetEntityComponent<DrawableComponent>(entity2);


            if (ComponentManager.Instance.CheckIfEntityHasComponent<AnimationComponent>(entity1))
            {
                AnimationComponent ani = ComponentManager.Instance.GetEntityComponent<AnimationComponent>(entity1);
                Source1 = ani.sourceRectangle;
            }
            else
            {
                Source1 = ent1.texture.Bounds;
            }

            if (ComponentManager.Instance.CheckIfEntityHasComponent<AnimationComponent>(entity2))
            {
                AnimationComponent ani = ComponentManager.Instance.GetEntityComponent<AnimationComponent>(entity2);
                Source2 = ani.sourceRectangle;
            }
            else
            {
                Source2 = ent2.texture.Bounds;
            }

            Color[] dataA = new Color[ent1.texture.Width * ent1.texture.Height];
            ent1.texture.GetData(dataA);
            Color[] dataB = new Color[ent2.texture.Width * ent2.texture.Height];
            ent2.texture.GetData(dataB);


            Color[] realDataA = GetImageData(dataA, ent1.texture.Width, Source1);
            Color[] realDataB = GetImageData(dataB, ent2.texture.Width, Source2);


            Rectangle its = Rectangle.Intersect(rectangleA, rectangleB);

            //// Check every point within the intersection bounds
            //for (int y = its.Top; y < its.Bottom; y++)
            //{
            //    for (int x = its.Left; x < its.Right; x++)
            //    {
            //        // Get the color of both pixels at this point
            //        Color colorA = realDataA[(x - Source1.Left) + (y - Source1.Top) * Source1.Width];
            //        Color colorB = realDataB[(x - Source2.Left) + (y - Source2.Top) * Source2.Width];

            //        // If both pixels are not completely transparent,
            //        if (colorA.A != 0 && colorB.A != 0)
            //        {
            //            // then an intersection has been found
            //            return true;
            //        }
            //    }
            //}
            for (int y = its.Top; y < its.Bottom; y++)
            {
                for (int x = its.Left; x < its.Right; x++)
                {
                    // Get the color of both pixels at this point
                    Color colorA = realDataA[(x - rectangleA.Left) + (y - rectangleA.Top) * rectangleA.Width];
                    Color colorB = realDataB[(x - rectangleB.Left) + (y - rectangleB.Top) * rectangleB.Width];

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="colorData"></param>
        /// <param name="width"> width of the texture </param>
        /// <param name="rectangle"></param>
        /// <returns></returns>
        Color[] GetImageData(Color[] colorData, int width, Rectangle rectangle)
        {
            Color[] color = new Color[rectangle.Width * rectangle.Height];
            for (int x = 0; x < rectangle.Width; x++)
                for (int y = 0; y < rectangle.Height; y++)
                    color[x + y * rectangle.Width] = colorData[x + rectangle.X + (y + rectangle.Y) * width];
            return color;
        }

    }
}