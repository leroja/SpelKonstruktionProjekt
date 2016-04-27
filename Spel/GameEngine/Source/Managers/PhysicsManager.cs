using GameEngine.Source.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Source.Managers
{
    class PhysicsManager
    {
        private static PhysicsManager instance;
        private bool physicsEnabled;
        private float GlobalGravity = 9.82f;

        private PhysicsManager()
        {
            physicsEnabled = true;
        }


        public static PhysicsManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PhysicsManager();
                }
                return instance;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="state">
        /// 
        /// </param>
        public void ChangePhysicState(bool state)
        {
            this.physicsEnabled = state;
        }



        ////////////////// fixa


        /// <summary>
        /// check if two entities has collided with each other
        /// using bounding rectangle
        /// </summary>
        /// <param name="entity1">
        /// 
        /// </param>
        /// <param name="entity2">
        /// 
        /// </param>
        /// <returns>
        /// true if bounding rectangle collision occured
        /// and false if no collision or physics is not enabled
        /// </returns>
        public bool RectangleCollision(int entity1, int entity2)
        {

            if (physicsEnabled)
            {
                CollisionRectangleComponent recA = ComponentManager.Instance.GetEntityComponent<CollisionRectangleComponent>(entity1);
                CollisionRectangleComponent recB = ComponentManager.Instance.GetEntityComponent<CollisionRectangleComponent>(entity2);


                Rectangle rectangleA = recA.CollisionRec;
                Rectangle rectangleB = recB.CollisionRec;


                if (rectangleA.Intersects(rectangleB))
                {
                    return true;
                }
            }


            return false;
        }


        /// <summary>
        /// check if two entities has collided with each other
        /// using pixel perfect
        /// </summary>
        /// <param name="entity1">
        /// 
        /// </param>
        /// <param name="entity2">
        /// 
        /// </param>
        /// <returns>
        /// true if pixel perfect collision occurred
        /// and false if no collision or physics is not enabled
        /// </returns>
        public bool PixelPerfectCollision(int entity1, int entity2)
        {

            if (physicsEnabled)
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
            else
            {
                return false;
            }

        }
    }
}
