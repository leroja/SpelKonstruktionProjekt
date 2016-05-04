using GameEngine.Source.Systems.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using GameEngine.Source.Managers;
using GameEngine.Source.Components;
using Spel.Source.Enum;
using Spel.Source.Components;
using GameEngine.Source.RandomStuff;

namespace Spel.Source.Systems
{
    class CollisionSystem : IUpdate
    {
        public void update(GameTime gameTime)
        {
            List<int> ents = ComponentManager.Instance.GetAllEntitiesWithComponentType<CollisionHappenedComponent>();

            if (ents != null)
            {
                foreach (var ent in ents)
                {
                    CollisionHappenedComponent c = ComponentManager.Instance.GetEntityComponent<CollisionHappenedComponent>(ent);

                    if (c.collisions != null)
                    {

                        var colls = RemoveDuplicateCollisions(c.collisions);

                        foreach (var coll in colls)
                        {

                            int ent1 = coll.entity1;
                            int ent2 = coll.entity2;

                            CollisionTypes type = CheckTypeOfCollision(ent1, ent2);


                            if (type == CollisionTypes.PlayerVsPlayer)
                            {
                                PositionComponent pos1 = ComponentManager.Instance.GetEntityComponent<PositionComponent>(ent1);
                                PositionComponent pos2 = ComponentManager.Instance.GetEntityComponent<PositionComponent>(ent2);
                                CollisionRectangleComponent crc1 = ComponentManager.Instance.GetEntityComponent<CollisionRectangleComponent>(ent1);
                                CollisionRectangleComponent crc2 = ComponentManager.Instance.GetEntityComponent<CollisionRectangleComponent>(ent2);


                                PlayerComponent pc1 = ComponentManager.Instance.GetEntityComponent<PlayerComponent>(ent1);
                                PlayerComponent pc2 = ComponentManager.Instance.GetEntityComponent<PlayerComponent>(ent2);

                                if (pos1.position.Y + crc1.CollisionRec.Height * 0.5f < pos2.position.Y)
                                {
                                    //Console.WriteLine(pc1.playerName + " is above  " + pc2.playerName);
                                    Console.WriteLine("Entity 1 is above entity 2");
                                }
                                else if (pos2.position.Y + crc2.CollisionRec.Height * 0.5f < pos1.position.Y)
                                {
                                    Console.WriteLine("Entity 2 is above entity 1");
                                }
                                else
                                {
                                    Console.WriteLine("Both on same level");
                                }

                                pos1.position = pos1.prevPosition;
                                pos2.position = pos2.prevPosition;

                            }
                            else if (type == CollisionTypes.PlayerVsWall)
                            {
                                if (ComponentManager.Instance.CheckIfEntityHasComponent<WallComponent>(ent1))
                                {
                                    CollisionRectangleComponent crc1 = ComponentManager.Instance.GetEntityComponent<CollisionRectangleComponent>(ent1);
                                    CollisionRectangleComponent crc2 = ComponentManager.Instance.GetEntityComponent<CollisionRectangleComponent>(ent2);
                                    PositionComponent pc = ComponentManager.Instance.GetEntityComponent<PositionComponent>(ent2);
                                    PositionComponent pcwall = ComponentManager.Instance.GetEntityComponent<PositionComponent>(ent1);
                                    WallComponent wall = ComponentManager.Instance.GetEntityComponent<WallComponent>(ent1);
                                    if (wall.wall == Wall.LeftWall)
                                    {
                                        if (crc2.CollisionRec.X + crc2.CollisionRec.Width * 0.5 < crc1.CollisionRec.X)
                                            pc.position.X = Game.Inst().GraphicsDevice.Viewport.Width - 1 - crc2.CollisionRec.Width * 0.5f;
                                    }
                                    else if (wall.wall == Wall.RightWall)
                                    {
                                        if (crc2.CollisionRec.X + crc2.CollisionRec.Width * 0.5 > crc1.CollisionRec.X)
                                            pc.position.X = 1 - crc2.CollisionRec.Width * 0.5f;
                                    }
                                    else if (wall.wall == Wall.TopWall)
                                    {

                                    }
                                    else if (wall.wall == Wall.BottomWall)
                                    {

                                    }
                                }

                            } else if (type == CollisionTypes.PlayerVsPowerup)
                            {

                            }
                            //PositionComponent pos3 = ComponentManager.Instance.GetEntityComponent<PositionComponent>(ent1);
                            //PositionComponent pos4 = ComponentManager.Instance.GetEntityComponent<PositionComponent>(ent2);
                            //pos3.position = pos3.prevPosition;
                            //pos4.position = pos4.prevPosition;
                        }
                    }
                    ComponentManager.Instance.RecycleID(ent);
                    ComponentManager.Instance.RemoveEntity(ent);
                }
            }
        }


        private CollisionTypes CheckTypeOfCollision(int ent1, int ent2)
        {
            List<IComponent> list1 = ComponentManager.Instance.GetAllEntityComponents(ent1);
            List<IComponent> list2 = ComponentManager.Instance.GetAllEntityComponents(ent2);


            if (list1.OfType<PlayerComponent>().Any() && list2.OfType<PlayerComponent>().Any())
            {
                return CollisionTypes.PlayerVsPlayer;
            }
            else if (list1.OfType<PlayerComponent>().Any() && list2.OfType<WallComponent>().Any() || list2.OfType<PlayerComponent>().Any() && list1.OfType<WallComponent>().Any())
            {
                return CollisionTypes.PlayerVsWall;
            }

            return CollisionTypes.NotDefined;
        }


        private List<Collision> RemoveDuplicateCollisions(List<Collision> collList)
        {

            List<Collision> colls = collList.Distinct(new CollisionComparer()).ToList();

            return colls;
        }
    }
}
