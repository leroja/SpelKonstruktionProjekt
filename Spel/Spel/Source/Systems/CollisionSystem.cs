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
    class CollisionSystem : IObserver
    {
        public void uppdate(IEvent t)
        {
            // @Todo
            // Maybe check that t really is a CollionEvent
            CollisionEvent coll = (CollisionEvent)t;   
            int ent1 = coll.entity1;
            int ent2 = coll.entity2;

            CollisionTypes collType = CheckTypeOfCollision(ent1, ent2);

            if (collType == CollisionTypes.PlayerVsPlayer)
            {
                PositionComponent pos1 = ComponentManager.Instance.GetEntityComponent<PositionComponent>(ent1);
                PositionComponent pos2 = ComponentManager.Instance.GetEntityComponent<PositionComponent>(ent2);
                CollisionRectangleComponent crc1 = ComponentManager.Instance.GetEntityComponent<CollisionRectangleComponent>(ent1);
                CollisionRectangleComponent crc2 = ComponentManager.Instance.GetEntityComponent<CollisionRectangleComponent>(ent2);

                PlayerComponent pc1 = ComponentManager.Instance.GetEntityComponent<PlayerComponent>(ent1);
                PlayerComponent pc2 = ComponentManager.Instance.GetEntityComponent<PlayerComponent>(ent2);

                if (pos1.position.Y + crc1.CollisionRec.Height * 0.5f < pos2.position.Y)
                { // entity 1 is above entity 2, entity 2 shall die or loose life

                    // @Temp
                    Console.WriteLine("Entity 1 is above entity 2");
                }
                else if (pos2.position.Y + crc2.CollisionRec.Height * 0.5f < pos1.position.Y)
                { // entity 2 is above entity 1, entity 1 shall die or loose life

                    // @Temp
                    Console.WriteLine("Entity 2 is above entity 1");
                }
                else // both are on the same "level", both shall die or loose life 
                {
                    // @Temp
                    Console.WriteLine("Both on same level");
                }

                pos1.position = pos1.prevPosition;
                pos2.position = pos2.prevPosition;

            }
            else if (collType == CollisionTypes.PlayerVsWall)
            {
                /// dela upp i en funktion
                if (ComponentManager.Instance.CheckIfEntityHasComponent<WallComponent>(ent1))
                {
                    CollisionRectangleComponent crc1 = ComponentManager.Instance.GetEntityComponent<CollisionRectangleComponent>(ent1);
                    CollisionRectangleComponent crc2 = ComponentManager.Instance.GetEntityComponent<CollisionRectangleComponent>(ent2);
                    PositionComponent pc = ComponentManager.Instance.GetEntityComponent<PositionComponent>(ent2);
                    PositionComponent pcwall = ComponentManager.Instance.GetEntityComponent<PositionComponent>(ent1);
                    WallComponent wall = ComponentManager.Instance.GetEntityComponent<WallComponent>(ent1);
                    //test
                    HealthComponent hc = ComponentManager.Instance.GetEntityComponent<HealthComponent>(ent1);
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
                    { // the playerEntity shall die or loose a life & possibly fall to the ground 

                        // @Temp
                        pc.position = pc.prevPosition;

                    }
                    else if (wall.wall == Wall.BottomWall)
                    { // the playerEntity shall die or loose a life

                        // @Temp
                        pc.position = pc.prevPosition;
                    }
                }
                if (ComponentManager.Instance.CheckIfEntityHasComponent<WallComponent>(ent2))
                {
                    CollisionRectangleComponent crc1 = ComponentManager.Instance.GetEntityComponent<CollisionRectangleComponent>(ent2);
                    CollisionRectangleComponent crc2 = ComponentManager.Instance.GetEntityComponent<CollisionRectangleComponent>(ent1);
                    PositionComponent pc = ComponentManager.Instance.GetEntityComponent<PositionComponent>(ent1);
                    PositionComponent pcwall = ComponentManager.Instance.GetEntityComponent<PositionComponent>(ent2);
                    WallComponent wall = ComponentManager.Instance.GetEntityComponent<WallComponent>(ent2);
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
                    { // the playerEntity shall die or loose a life & possibly fall to the ground 

                        // @Temp
                        pc.position = pc.prevPosition;

                    }
                    else if (wall.wall == Wall.BottomWall)
                    { // the playerEntity shall die or loose a life

                        // @Temp
                        pc.position = pc.prevPosition;
                    }
                }
            }
            else if (collType == CollisionTypes.PlayerVsPowerup)
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
