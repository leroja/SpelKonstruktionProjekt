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
using GameEngine.Source.Components;

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
                        foreach (var coll in c.collisions)
                        {

                            int ent1 = coll.entity1;
                            int ent2 = coll.entity2;

                            CollisionTypes type = CheckTypeOfCollision(ent1, ent2);


                            if (type == CollisionTypes.PlayerVsPlayer)
                            {
                                if (ComponentManager.Instance.CheckIfEntityHasComponent<WallComponent>(ent1))
                                {
                                    WallComponent wall = ComponentManager.Instance.GetEntityComponent<WallComponent>(ent1);
                                    if (wall.wall == Wall.LeftWall)
                                    {

                                    }
                                    else if (wall.wall == Wall.RightWall)
                                    {

                                    }
                                }

                                PositionComponent pos = ComponentManager.Instance.GetEntityComponent<PositionComponent>(ent1);
                                pos.position = pos.prevPosition;

                                PositionComponent pos1 = ComponentManager.Instance.GetEntityComponent<PositionComponent>(ent2);
                                pos1.position = pos1.prevPosition;

                            }
                            else if (type == CollisionTypes.PlayerVsWall)
                            {
                                PositionComponent pos = ComponentManager.Instance.GetEntityComponent<PositionComponent>(ent1);
                                pos.position = pos.prevPosition;

                                PositionComponent pos1 = ComponentManager.Instance.GetEntityComponent<PositionComponent>(ent2);
                                pos1.position = pos1.prevPosition;

                            }
                            else if (type == CollisionTypes.PlayerVsPowerup)
                            {
                                int player;
                                int power;
                                List<IComponent> list1 = ComponentManager.Instance.GetAllEntityComponents(ent1);
                                List<IComponent> list2 = ComponentManager.Instance.GetAllEntityComponents(ent2);
                                if (list1.OfType<PlayerComponent>().Any())
                                {
                                    player = ent1;
                                    power = ent2;
                                }
                                else
                                {
                                    player = ent2;
                                    power = ent1;
                                }
                                PowerUppComponent tes = ComponentManager.Instance.GetEntityComponent<PowerUppComponent>(power);
                                switch (tes.type)
                                {
                                    case 1:
                                        BallOfSpikesSystem temp = new BallOfSpikesSystem();
                                        temp.OnPowerUpPicup(player);
                                        break;
                                    default:
                                        break;
                                }
                                rec(ent1, ent2);
                            }
                            
                            ComponentManager.Instance.RecycleID(ent);
                            ComponentManager.Instance.RemoveEntity(ent);
                        }
                    }
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
            else if (list1.OfType<PlayerComponent>().Any() && list2.OfType<PowerUppComponent>().Any() || list2.OfType<PlayerComponent>().Any() && list1.OfType<PowerUppComponent>().Any())
            {
                return CollisionTypes.PlayerVsPowerup;
            }
            return CollisionTypes.NotDefined;
        }

        /// <summary>
        /// Removes Powerup from component manager
        /// </summary>
        /// <param name="tmep1"></param>
        /// <param name="temp2"></param>
        private void rec(int tmep1,int temp2)
        {
            List<IComponent> list1 = ComponentManager.Instance.GetAllEntityComponents(tmep1);
            List<IComponent> list2 = ComponentManager.Instance.GetAllEntityComponents(temp2);
            if (list1.OfType<PlayerComponent>().Any())
            {
                ComponentManager.Instance.RecycleID(temp2);
                ComponentManager.Instance.RemoveEntity(temp2);
            }
            else
            {
                ComponentManager.Instance.RecycleID(tmep1);
                ComponentManager.Instance.RemoveEntity(tmep1);
            }
        }
    }
}
