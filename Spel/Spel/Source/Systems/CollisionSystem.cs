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

                    HealthComponent hc = ComponentManager.Instance.GetEntityComponent<HealthComponent>(ent2);
                    hc.health -= 1; // något annat kanske
                }
                else if (pos2.position.Y + crc2.CollisionRec.Height * 0.5f < pos1.position.Y)
                { // entity 2 is above entity 1, entity 1 shall die or loose life

                    // @Temp
                    Console.WriteLine("Entity 2 is above entity 1");

                    HealthComponent hc = ComponentManager.Instance.GetEntityComponent<HealthComponent>(ent1);
                    hc.health -= 1; // något annat kanske
                }
                else // both are on the same "level", both shall die or loose life 
                {
                    // @Temp
                    Console.WriteLine("Both on same level");

                    HealthComponent hc1 = ComponentManager.Instance.GetEntityComponent<HealthComponent>(ent1);
                    HealthComponent hc2 = ComponentManager.Instance.GetEntityComponent<HealthComponent>(ent2);
                    hc1.health -= 1; // något annat kanske
                    hc2.health -= 1;
                }

                pos1.position = pos1.prevPosition;
                pos2.position = pos2.prevPosition;

            }
            else if (collType == CollisionTypes.PlayerVsWall)
            {
                if (ComponentManager.Instance.CheckIfEntityHasComponent<WallComponent>(ent1))
                {
                    PlayerVsWallColl(ent2, ent1);
                }
                if (ComponentManager.Instance.CheckIfEntityHasComponent<WallComponent>(ent2))
                {
                    PlayerVsWallColl(ent1, ent2);
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
                    case 2:
                        HealthComponent hp = ComponentManager.Instance.GetEntityComponent<HealthComponent>(player);
                        hp.health = hp.maxhealth;
                        break;
                    default:
                        break;
                }
                rec(ent1, ent2);
            }else if(collType == CollisionTypes.PlayerVsPlatform)
            {
                if (ComponentManager.Instance.CheckIfEntityHasComponent<PlatformComponent>(ent1))
                {
                    PlayerVsPlatformColl(ent2, ent1);
                }
                if (ComponentManager.Instance.CheckIfEntityHasComponent<PlatformComponent>(ent2))
                {
                    PlayerVsPlatformColl(ent1, ent2);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Player"></param>
        /// <param name="WallEnt"></param>
        private void PlayerVsWallColl(int Player, int WallEnt)
        {
            CollisionRectangleComponent crc1 = ComponentManager.Instance.GetEntityComponent<CollisionRectangleComponent>(WallEnt);
            CollisionRectangleComponent crc2 = ComponentManager.Instance.GetEntityComponent<CollisionRectangleComponent>(Player);
            PositionComponent pc = ComponentManager.Instance.GetEntityComponent<PositionComponent>(Player);
            PositionComponent pcwall = ComponentManager.Instance.GetEntityComponent<PositionComponent>(WallEnt);
            WallComponent wall = ComponentManager.Instance.GetEntityComponent<WallComponent>(WallEnt);
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
            { // the playerEntity shall loose ONE life & possibly fall to the ground 

                // @Temp
                pc.position = pc.prevPosition;


                HealthComponent hc = ComponentManager.Instance.GetEntityComponent<HealthComponent>(Player);
                hc.health -= 1; // något annat kanske?
                // mer saker?
            }
            else if (wall.wall == Wall.BottomWall)
            { // the playerEntity shall loose ONE life

                // @Temp
                pc.position = pc.prevPosition;

                HealthComponent hc = ComponentManager.Instance.GetEntityComponent<HealthComponent>(Player);
                hc.health -= 1; // något annat kanske?
                // mer saker?
            }
        }

        private void PlayerVsPlatformColl(int Player, int Platform)
        {
            PositionComponent ppc = ComponentManager.Instance.GetEntityComponent<PositionComponent>(Player);
            PositionComponent pfpc = ComponentManager.Instance.GetEntityComponent<PositionComponent>(Platform);
            CollisionRectangleComponent pcrc = ComponentManager.Instance.GetEntityComponent<CollisionRectangleComponent>(Player);
            CollisionRectangleComponent pfcrc = ComponentManager.Instance.GetEntityComponent<CollisionRectangleComponent>(Platform);
            PlatformComponent pc = ComponentManager.Instance.GetEntityComponent<PlatformComponent>(Platform);

            if (pcrc.CollisionRec.Intersects(pc.TopRec))// kolla om spelarn landar på plattformen
            {
                Console.WriteLine("test");
                ppc.position = ppc.prevPosition;
                // spelarn ska kunna stanna och sedan hoppa iväg åt motsatt håll
                // vad som krävs;
                // kraften åt sidan måste motverkas
                // samma sak med gravitationen 
            }
            else // om man inte landar ovan på plattformen så ska samma sak ske,,, om jag har tänkt rätt
            {
                ppc.position = ppc.prevPosition;
            }



            //else if ()// kolla om spelarn hoppar in i plattformen underifrån
            //{
            //    // spelarn ska förlora liv 

            //    // + några mer saker
            //}

            // kolla om spelarn hoppar in i höger sidan

            // kolla om spelarn hoppar in i vänstersidan
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
            else if (list1.OfType<PlayerComponent>().Any() && list2.OfType<PlatformComponent>().Any() || list2.OfType<PlayerComponent>().Any() && list1.OfType<PlatformComponent>().Any())
            {
                return CollisionTypes.PlayerVsPlatform;
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
