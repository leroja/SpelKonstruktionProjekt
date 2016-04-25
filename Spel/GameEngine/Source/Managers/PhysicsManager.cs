using GameEngine.Source.Components;
using GameEngine.Source.EntityStuff;
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


        private PhysicsManager()
        {

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


        public bool RectangleCollision(Entity entity1, Entity entity2)
        {
            
            CollisionRectangleComponent recA = ComponentManager.Instance.GetEntityComponent<CollisionRectangleComponent>(entity1);
            CollisionRectangleComponent recB = ComponentManager.Instance.GetEntityComponent<CollisionRectangleComponent>(entity2);


            Rectangle rectangleA = recA.CollisionRec;
            Rectangle rectangleB = recB.CollisionRec;


            if (rectangleA.Intersects(rectangleB))
            {
                return true;
            }
                


            return false;
        }


        public bool PixlePerfectCollision(Entity entity1, Entity entity2)
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
            for(int y = its.Top; y < its.Bottom; y++)
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

            // No intersection found
            return false;
            
        }
    }
}
