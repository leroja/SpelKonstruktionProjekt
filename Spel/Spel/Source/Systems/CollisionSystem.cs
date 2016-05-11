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
        public void update(IEvent t)
        {
            // @Todo
            // Maybe check that t really is a CollionEvent
            CollisionEvent coll = (CollisionEvent)t;   
            int ent1 = coll.entity1;
            int ent2 = coll.entity2;
            GameTime gt = coll.gt;

            CollisionTypes collType = CheckTypeOfCollision(ent1, ent2);

            if (collType == CollisionTypes.PlayerVsPlayer)
            {
                PositionComponent pos1 = ComponentManager.Instance.GetEntityComponent<PositionComponent>(ent1);
                PositionComponent pos2 = ComponentManager.Instance.GetEntityComponent<PositionComponent>(ent2);
                CollisionRectangleComponent crc1 = ComponentManager.Instance.GetEntityComponent<CollisionRectangleComponent>(ent1);
                CollisionRectangleComponent crc2 = ComponentManager.Instance.GetEntityComponent<CollisionRectangleComponent>(ent2);
                CollisionComponent cc1 = ComponentManager.Instance.GetEntityComponent<CollisionComponent>(ent1);
                CollisionComponent cc2 = ComponentManager.Instance.GetEntityComponent<CollisionComponent>(ent2);
                DirectionComponent dcp1 = ComponentManager.Instance.GetEntityComponent<DirectionComponent>(ent1);
                DirectionComponent dcp2 = ComponentManager.Instance.GetEntityComponent<DirectionComponent>(ent2);
                VelocityComponent vcp1 = ComponentManager.Instance.GetEntityComponent<VelocityComponent>(ent1);
                VelocityComponent vcp2 = ComponentManager.Instance.GetEntityComponent<VelocityComponent>(ent2);


                PlayerComponent pc1 = ComponentManager.Instance.GetEntityComponent<PlayerComponent>(ent1);
                PlayerComponent pc2 = ComponentManager.Instance.GetEntityComponent<PlayerComponent>(ent2);

                if (pos1.position.Y + crc1.CollisionRec.Height * 0.5f < pos2.position.Y)
                { // entity 1 is above entity 2, entity 2 shall loose life


                    // entity2 shall loose One life
                    // entity2 shall then fall to the ground, not colliding with anything on the way down & it shall not be able to move
                    // when on the ground it shall not move or be afected by the gravity & sidemovement for a duration


                    // @Temp
                    // Console.WriteLine("Entity 1 is above entity 2");
                    if (dcp2.directio != Direction.Still)
                    {
                        dcp2.preDir = dcp2.directio;
                        dcp2.directio = Direction.Still;
                    }
                    vcp2.velocity.Y = 0;

                    HealthComponent hc = ComponentManager.Instance.GetEntityComponent<HealthComponent>(ent2);
                    hc.health -= 1;
                }
                else if (pos2.position.Y + crc2.CollisionRec.Height * 0.5f < pos1.position.Y)
                {   // entity 2 is above entity 1, entity 1 shall loose life


                    // entity 1 shall loose ONE life
                    // entity 1 shall then fall to the ground, not colliding with anything on the way down
                    // when on the ground it shall not move or be afected by the gravity & sidemovement for a duration


                    // entity 2 shall not loose a life & continue as normal


                    // @Temp
                    //Console.WriteLine("Entity 2 is above entity 1");
                    if (dcp1.directio != Direction.Still)
                    {
                        dcp1.preDir = dcp1.directio;
                        dcp1.directio = Direction.Still;
                    }
                    vcp1.velocity.Y = 0;


                    HealthComponent hc = ComponentManager.Instance.GetEntityComponent<HealthComponent>(ent1);
                    hc.health -= 1;
                }
                else // both are on the same "level",both shall loose a life 
                {


                    // both shall loose ONE life
                    // then they shall fall to the ground, not colliding with each other
                    // 


                    // @Temp
                    //Console.WriteLine("Both on same level");
                    if (dcp1.directio != Direction.Still)
                    {
                        dcp1.preDir = dcp1.directio;
                        dcp1.directio = Direction.Still;
                    }
                    vcp1.velocity.Y = 0;
                    if (dcp2.directio != Direction.Still)
                    {
                        dcp2.preDir = dcp2.directio;
                        dcp2.directio = Direction.Still;
                    }
                    vcp2.velocity.Y = 0;

                    HealthComponent hc1 = ComponentManager.Instance.GetEntityComponent<HealthComponent>(ent1);
                    HealthComponent hc2 = ComponentManager.Instance.GetEntityComponent<HealthComponent>(ent2);
                    hc1.health -= 1;
                    hc2.health -= 1;
                }

                //pos1.position = pos1.prevPosition;
                //pos2.position = pos2.prevPosition;

            }
            else if (collType == CollisionTypes.PlayerVsWall)
            {
                if (ComponentManager.Instance.CheckIfEntityHasComponent<WallComponent>(ent1))
                {
                    PlayerVsWallColl(ent2, ent1, gt);
                }
                if (ComponentManager.Instance.CheckIfEntityHasComponent<WallComponent>(ent2))
                {
                    PlayerVsWallColl(ent1, ent2, gt);
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
            }else if(collType == CollisionTypes.PlayerVsPlatform)
            {
                if (ComponentManager.Instance.CheckIfEntityHasComponent<PlatformComponent>(ent1))
                {
                    PlayerVsPlatformColl(ent2, ent1, gt);
                }
                if (ComponentManager.Instance.CheckIfEntityHasComponent<PlatformComponent>(ent2))
                {
                    PlayerVsPlatformColl(ent1, ent2, gt);
                }
            }else if(collType == CollisionTypes.NotDefined)
            {

            }
        }

        /// <summary>
        /// Handles Player versus Wall collision
        /// </summary>
        /// <param name="Player">
        /// id of the player entity
        /// </param>
        /// <param name="WallEnt">
        /// id of the wall entity
        /// </param>
        private void PlayerVsWallColl(int Player, int WallEnt, GameTime gameTime)
        {
            VelocityComponent pvc = ComponentManager.Instance.GetEntityComponent<VelocityComponent>(Player);
            DirectionComponent pdc = ComponentManager.Instance.GetEntityComponent<DirectionComponent>(Player);
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
            {

                // the playerEntity shall loose ONE life & fall to the ground 
                // 
                // stop the side movement
                // stop gravity
                //


                // @Temp


                if (pdc.directio != Direction.Still)
                {
                    pdc.preDir = pdc.directio;
                    pdc.directio = Direction.Still;
                }
                pvc.velocity.Y = 0;
                pvc.velocity.Y += 500 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                //pc.position = pc.prevPosition;


                HealthComponent hc = ComponentManager.Instance.GetEntityComponent<HealthComponent>(Player);
                hc.health -= 1;
            }
            else if (wall.wall == Wall.BottomWall)
            {

                // the player Entity shall loose ONE life & not fall through the ground...
                // stop the side movement
                // stop gravity
                // turn of the plays ability to control the movement for a short duration


                // @Temp
                //pc.position = pc.prevPosition;

                if (pdc.directio != Direction.Still)
                {
                    pdc.preDir = pdc.directio;
                    pdc.directio = Direction.Still;
                }
                pvc.velocity.Y = 0;
                pvc.velocity.Y -= 500F * (float)gameTime.ElapsedGameTime.TotalSeconds;

                
                HealthComponent hc = ComponentManager.Instance.GetEntityComponent<HealthComponent>(Player);
                hc.health -= 1;
            }
        }

        /// <summary>
        /// handles player versus platform collision
        /// </summary>
        /// <param name="Player">
        /// id of the player entity
        /// </param>
        /// <param name="Platform">
        /// id of the platform entity
        /// </param>
        private void PlayerVsPlatformColl(int Player, int Platform, GameTime gameTime)
        {
            DirectionComponent dc = ComponentManager.Instance.GetEntityComponent<DirectionComponent>(Player);
            VelocityComponent pvc = ComponentManager.Instance.GetEntityComponent<VelocityComponent>(Player);
            PositionComponent ppc = ComponentManager.Instance.GetEntityComponent<PositionComponent>(Player);
            PositionComponent pfpc = ComponentManager.Instance.GetEntityComponent<PositionComponent>(Platform);
            CollisionRectangleComponent pcrc = ComponentManager.Instance.GetEntityComponent<CollisionRectangleComponent>(Player);
            CollisionRectangleComponent pfcrc = ComponentManager.Instance.GetEntityComponent<CollisionRectangleComponent>(Platform);
            PlatformComponent pc = ComponentManager.Instance.GetEntityComponent<PlatformComponent>(Platform);

            if (pcrc.CollisionRec.Intersects(pc.TopRec)) // @TODO tänk om gällande pixlePerfect, kanske
            {


                // make the player stand still
                // stop the gravity for the player
                // stop the side movement
                // 
                // change the direction

                //@temp
                changeDir(dc);
                if (dc.directio != Direction.Still)
                {
                    dc.preDir = dc.directio;
                    dc.directio = Direction.Still;
                }
                pvc.velocity.Y = 0;
                pvc.velocity.Y -= 500* (float)gameTime.ElapsedGameTime.TotalSeconds;


                //Console.WriteLine("test");
                //ppc.position = ppc.prevPosition;
            }
            else
            {

                // player shall loose ONE life
                // & then fall to the ground
                // 
                
                if (dc.directio != Direction.Still)
                {
                    dc.preDir = dc.directio;
                    dc.directio = Direction.Still;
                }
                pvc.velocity.Y = 0;
                pvc.velocity.Y += 500 * (float)gameTime.ElapsedGameTime.TotalSeconds;

                // @temp
                HealthComponent hc = ComponentManager.Instance.GetEntityComponent<HealthComponent>(Player);
                hc.health -= 1;
                ppc.position = ppc.prevPosition;
            }
        }


        /// <summary>
        /// determinates what kind of collision has occured based on the two entities
        /// </summary>
        /// <param name="ent1">
        /// Id of entity 1
        /// </param>
        /// <param name="ent2">
        /// id of entity 2
        /// </param>
        /// <returns>
        /// Collision type
        /// </returns>
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
        /// Changes the direction of the component
        /// and sets the previous direction
        /// </summary>
        /// <param name="dc"></param>
        private void changeDir(DirectionComponent dc)
        {
            if(dc.directio == Direction.Left)
            {
                dc.preDir = dc.directio;
                dc.directio = Direction.Right;
            }else if(dc.directio == Direction.Right)
            {
                dc.preDir = dc.directio;
                dc.directio = Direction.Left;
            }
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
