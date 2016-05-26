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
    /// <summary>
    /// A system for handling collisions when they have occurred
    /// </summary>
    class CollisionSystem : IObserver, IObserving
    {
        public void update(IEvent t)
        {
            if (t.GetType() == typeof(CollisionEvent))
            {

                CollisionEvent coll = (CollisionEvent)t;

                int ent1 = coll.entity1;
                int ent2 = coll.entity2;
                GameTime gt = coll.gt;

                CollisionTypes collType = CheckTypeOfCollision(ent1, ent2);

                if (collType == CollisionTypes.PlayerVsPlayer)
                {
                    PlayerVsPlayerCollision(ent1, ent2, gt);
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
                else if (collType == CollisionTypes.PlayerVsCube)
                {
                    if (ComponentManager.Instance.CheckIfEntityHasComponent<PlayerComponent>(ent1))
                    {
                        PlayerVsCubeColl(ent1, ent2);
                    }
                    else if (ComponentManager.Instance.CheckIfEntityHasComponent<PlayerComponent>(ent2))
                    {
                        PlayerVsCubeColl(ent2, ent1);
                    }
                }
                else if (collType == CollisionTypes.PlayerVsPowerup)
                {
                    PlayerVSPowerup(ent1, ent2);
                }
                else if (collType == CollisionTypes.PlayerVsPlatform)
                {
                    if (ComponentManager.Instance.CheckIfEntityHasComponent<PlatformComponent>(ent1))
                    {
                        PlayerVsPlatformColl(ent2, ent1, gt);
                    }
                    if (ComponentManager.Instance.CheckIfEntityHasComponent<PlatformComponent>(ent2))
                    {
                        PlayerVsPlatformColl(ent1, ent2, gt);
                    }
                }
                else if (collType == CollisionTypes.NotDefined)
                {
                    //@Todo maybe do something here or throw some kind of exception
                }             
            }
        }

        /// <summary>
        /// Handles Player versus Wall collision
        /// </summary>
        /// <param name="Player"> Id of the player entity </param>
        /// <param name="WallEnt"> Id of the wall entity </param>
        private void PlayerVsWallColl(int Player, int WallEnt, GameTime gameTime)
        {
            PlayerComponent playerComp = ComponentManager.Instance.GetEntityComponent<PlayerComponent>(Player);
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
                    pc.position.X = Game.Instance.GraphicsDevice.Viewport.Width - 1 - crc2.CollisionRec.Width * 0.5f;
            }
            else if (wall.wall == Wall.RightWall)
            {
                if (crc2.CollisionRec.X + crc2.CollisionRec.Width * 0.5 > crc1.CollisionRec.X)
                    pc.position.X = 1 - crc2.CollisionRec.Width * 0.5f;
            }
            else if (wall.wall == Wall.TopWall && !playerComp.isFalling)
            {
                if (pdc.directio != Direction.Still)
                {
                    changeDir(pdc);
                    pdc.preDir = pdc.directio;
                    pdc.directio = Direction.Still;
                }
                pvc.velocity.Y = 0;
                pvc.velocity.Y += 500 * (float)gameTime.ElapsedGameTime.TotalSeconds;

                playerComp.isFalling = true;

                HealthComponent hc = ComponentManager.Instance.GetEntityComponent<HealthComponent>(Player);
                //hc.health -= 1;
            }
            else if (wall.wall == Wall.BottomWall)
            {
                //@TODO 
                // only loose life when the player jump on the floor, not when the player falls to the ground
                // and have some kind of timer unti you can jump away

                OnFloorComponent fc = ComponentManager.Instance.GetEntityComponent<OnFloorComponent>(Player);

                playerComp.isFalling = false;


                if (pdc.directio != Direction.Still)
                {
                    pvc.velocity.Y = 0;
                    changeDir(pdc);
                    pdc.preDir = pdc.directio;
                    pdc.directio = Direction.Still;
                }
                //else if(pdc.directio == Direction.Still)
                //{
                //    pvc.velocity.Y = 0;
                //}
                pvc.velocity.Y = 0;
                pvc.velocity.Y -= 500F * (float)gameTime.ElapsedGameTime.TotalSeconds;

                HealthComponent hc = ComponentManager.Instance.GetEntityComponent<HealthComponent>(Player);

                if (fc == null)
                {
                    fc = new OnFloorComponent();
                    ComponentManager.Instance.AddComponentToEntity(Player, fc);
                    hc.health -= 1;
                    ComponentManager.Instance.AddComponentToEntity(Player, new SoundEffectComponent("splat"));
                }
                
            }
        }

        /// <summary>
        /// Handles player versus platform collision
        /// </summary>
        /// <param name="Player"> Id of the player entity </param>
        /// <param name="Platform"> Id of the platform entity </param>
        private void PlayerVsPlatformColl(int Player, int Platform, GameTime gameTime)
        {
            PlayerComponent playComp = ComponentManager.Instance.GetEntityComponent<PlayerComponent>(Player);
            DirectionComponent dc = ComponentManager.Instance.GetEntityComponent<DirectionComponent>(Player);
            VelocityComponent pvc = ComponentManager.Instance.GetEntityComponent<VelocityComponent>(Player);
            PositionComponent ppc = ComponentManager.Instance.GetEntityComponent<PositionComponent>(Player);
            CollisionRectangleComponent pcrc = ComponentManager.Instance.GetEntityComponent<CollisionRectangleComponent>(Player);
            PlatformComponent pc = ComponentManager.Instance.GetEntityComponent<PlatformComponent>(Platform);
            HealthComponent hc = ComponentManager.Instance.GetEntityComponent<HealthComponent>(Player);

            if (!playComp.isFalling)
            {
                if (pcrc.CollisionRec.Intersects(pc.TopRec)) // @TODO tänk om gällande pixlePerfect, kanske
                {
                    //@todo hur fan ska man ta bort n om spelarn hoppar ifrån eller blir nerputtad 
                    if (!ComponentManager.Instance.CheckIfEntityHasComponent<TTLComponent>(Player))
                    {
                        TTLComponent ttl = new TTLComponent(5f);
                        ComponentManager.Instance.AddComponentToEntity(Player, ttl);
                    }
                    else
                    {
                        TTLComponent ttl = ComponentManager.Instance.GetEntityComponent<TTLComponent>(Player);
                        if(ttl.curTime >= ttl.maxTime)
                        {
                            playComp.isFalling = true;
                            //hc.health -= 1;
                            ComponentManager.Instance.RemoveComponentFromEntity(Player, ttl);
                        }
                    }

                    changeDir(dc);
                    if (dc.directio != Direction.Still)
                    {
                        dc.preDir = dc.directio;
                        dc.directio = Direction.Still;
                    }
                    pvc.velocity.Y = 0;
                    pvc.velocity.Y -= 500 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                else
                {
                    if (dc.directio != Direction.Still)
                    {
                        changeDir(dc);
                        dc.preDir = dc.directio;
                        dc.directio = Direction.Still;
                    }
                    pvc.velocity.Y = 0;
                    pvc.velocity.Y += 500 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    playComp.isFalling = true;

                    ComponentManager.Instance.AddComponentToEntity(Player, new SoundEffectComponent("pfhit"));
                    //hc.health -= 1;
                }
            }
        }


        /// <summary>
        /// determinates what kind of collision has occured based on the two entities
        /// </summary>
        /// <param name="ent1"> Id of entity 1 </param>
        /// <param name="ent2"> Id of entity 2 </param>
        /// <returns> Type of the collision </returns>
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
            else if (list1.OfType<PlayerComponent>().Any() && list2.OfType<ChangeCubeComponent>().Any() || list2.OfType<PlayerComponent>().Any() && list1.OfType<ChangeCubeComponent>().Any())
            {
                return CollisionTypes.PlayerVsCube;
            }
            return CollisionTypes.NotDefined;
        }

        /// <summary>
        /// Changes the direction of the component
        /// and sets the previous direction to the current direction
        /// </summary>
        /// <param name="dc"></param>
        private void changeDir(DirectionComponent dc)
        {
            if(dc.directio == Direction.Left)
            {
                dc.preDir = dc.directio;
                dc.directio = Direction.Right;
            }
            else if(dc.directio == Direction.Right)
            {
                dc.preDir = dc.directio;
                dc.directio = Direction.Left;
            }
        }

        /// <summary>
        /// pushes the two players away from each other
        /// so that they don't are inside each other when they are on the floor
        /// </summary>
        /// <param name="player1"> id of player 1 </param>
        /// <param name="player2"> id of player 2 </param>
        private void pushAway(int player1, int player2, GameTime gameTime)
        {
            PositionComponent pos1 = ComponentManager.Instance.GetEntityComponent<PositionComponent>(player1);
            PositionComponent pos2 = ComponentManager.Instance.GetEntityComponent<PositionComponent>(player2);
            DirectionComponent dc1 = ComponentManager.Instance.GetEntityComponent<DirectionComponent>(player1);
            DirectionComponent dc2 = ComponentManager.Instance.GetEntityComponent<DirectionComponent>(player2);
            float push = 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (pos1.position.X > pos2.position.X)
            {
                pos1.position.X += push;
                pos2.position.X -= push;

                if (dc1.directio == Direction.Left)
                    changeDir(dc1);
                if (dc2.directio == Direction.Right)
                    changeDir(dc2);
            }
            else
            {
                pos1.position.X -= push;
                pos2.position.X += push;
                if (dc2.directio == Direction.Left)
                    changeDir(dc2);
                if (dc1.directio == Direction.Right)
                    changeDir(dc1);
            }
        }

        /// <summary>
        /// handles player versus changeDirectionCube collision
        /// </summary>
        /// <param name="Player"> Id of the player entity </param>
        /// <param name="Cube"> Id of the Cube entity </param>
        private void PlayerVsCubeColl(int Player, int Cube)
        {
            PlayerComponent plaCom = ComponentManager.Instance.GetEntityComponent<PlayerComponent>(Player);
            ChangeCubeComponent change = ComponentManager.Instance.GetEntityComponent<ChangeCubeComponent>(Cube);
            if (!plaCom.isFalling && change.time > 0.5F)
            {
                ComponentManager.Instance.AddComponentToEntity(Player, new SoundEffectComponent("signhit"));
                DirectionComponent dir = ComponentManager.Instance.GetEntityComponent<DirectionComponent>(Player);
                changeDir(dir);
 

                change.isTaken = true;
            }
        }


        /// <summary>
        /// A method for handling player versus player collision
        /// </summary>
        /// <param name="ent1"> ID of entity 1 </param>
        /// <param name="ent2"> ID of entity 2 </param>
        /// <param name="gt"></param>
        private void PlayerVsPlayerCollision(int ent1, int ent2, GameTime gt)
        {
            PlayerComponent pcp1 = ComponentManager.Instance.GetEntityComponent<PlayerComponent>(ent1);
            PlayerComponent pcp2 = ComponentManager.Instance.GetEntityComponent<PlayerComponent>(ent2);
            PositionComponent pos1 = ComponentManager.Instance.GetEntityComponent<PositionComponent>(ent1);
            PositionComponent pos2 = ComponentManager.Instance.GetEntityComponent<PositionComponent>(ent2);
            CollisionRectangleComponent crc1 = ComponentManager.Instance.GetEntityComponent<CollisionRectangleComponent>(ent1);
            CollisionRectangleComponent crc2 = ComponentManager.Instance.GetEntityComponent<CollisionRectangleComponent>(ent2);
            DirectionComponent dcp1 = ComponentManager.Instance.GetEntityComponent<DirectionComponent>(ent1);
            DirectionComponent dcp2 = ComponentManager.Instance.GetEntityComponent<DirectionComponent>(ent2);
            VelocityComponent vcp1 = ComponentManager.Instance.GetEntityComponent<VelocityComponent>(ent1);
            VelocityComponent vcp2 = ComponentManager.Instance.GetEntityComponent<VelocityComponent>(ent2);
            BallOfSpikesPowerUpComponent bspc1 = ComponentManager.Instance.GetEntityComponent<BallOfSpikesPowerUpComponent>(ent1);
            BallOfSpikesPowerUpComponent bspc2 = ComponentManager.Instance.GetEntityComponent<BallOfSpikesPowerUpComponent>(ent2);

            if (pos1.position.Y + crc1.CollisionRec.Height * 0.5f < pos2.position.Y)
            { // entity 1 is above entity 2
                if (!pcp1.isFalling && !pcp2.isFalling)
                {
                    if (dcp2.directio != Direction.Still)
                    {
                        changeDir(dcp2);
                        dcp2.preDir = dcp2.directio;
                        dcp2.directio = Direction.Still;
                    }
                    vcp2.velocity.Y = 0;
                    vcp1.velocity.Y = -200f;
                    ComponentManager.Instance.AddComponentToEntity(ent2, new SoundEffectComponent("hit"));
                    ComponentManager.Instance.AddComponentToEntity(ent1, new SoundEffectComponent("grunt"));

                    //if enitity 2 dosent have ballofspikePUPcomponent loose life
                    if (bspc2 == null)
                    {
                        HealthComponent hc2 = ComponentManager.Instance.GetEntityComponent<HealthComponent>(ent2);
                        //hc2.health -= 1;
                        pcp2.isFalling = true;
                    }
                    //else if enitity 1 dosent have ballofspikePUPcomponent loose life
                    else if (bspc1 == null)
                    {
                        HealthComponent hc1 = ComponentManager.Instance.GetEntityComponent<HealthComponent>(ent1);
                        //hc1.health -= 1;
                        pcp1.isFalling = true;
                    }
                }
            }
            else if (pos2.position.Y + crc2.CollisionRec.Height * 0.5f < pos1.position.Y)
            {   // entity 2 is above entity 1
                if (!pcp1.isFalling && !pcp2.isFalling)
                {
                    if (dcp1.directio != Direction.Still)
                    {
                        changeDir(dcp1);
                        dcp1.preDir = dcp1.directio;
                        dcp1.directio = Direction.Still;
                    }
                    vcp1.velocity.Y = 0;
                    vcp2.velocity.Y = -200f;

                    ComponentManager.Instance.AddComponentToEntity(ent1, new SoundEffectComponent("hit"));
                    ComponentManager.Instance.AddComponentToEntity(ent2, new SoundEffectComponent("grunt"));
                    
                    //if enitity 1 dosent have ballofspikePUPcomponent loose life
                    if (bspc1 == null)
                    {
                        HealthComponent hc1 = ComponentManager.Instance.GetEntityComponent<HealthComponent>(ent1);
                        //hc1.health -= 1;
                        pcp1.isFalling = true;
                    }
                    //else if enitity 2 dosent have ballofspikePUPcomponent loose life
                    else if (bspc2 == null)
                    {
                        HealthComponent hc2 = ComponentManager.Instance.GetEntityComponent<HealthComponent>(ent2);
                        //hc2.health -= 1;
                        pcp2.isFalling = true;
                    }
                }
            }
            else // both are on the same "level" 
            {
                if (!pcp2.isFalling && !pcp1.isFalling)
                {
                    if (dcp1.directio != Direction.Still)
                    {
                        changeDir(dcp1);
                        dcp1.preDir = dcp1.directio;
                        dcp1.directio = Direction.Still;
                    }
                    vcp1.velocity.Y = 0;
                    if (dcp2.directio != Direction.Still)
                    {
                        changeDir(dcp2);
                        dcp2.preDir = dcp2.directio;
                        dcp2.directio = Direction.Still;
                    }
                    vcp2.velocity.Y = 0;

                   ComponentManager.Instance.AddComponentToEntity(ent2, new SoundEffectComponent("sidehit"));
                   ComponentManager.Instance.AddComponentToEntity(ent1, new SoundEffectComponent("sidehit"));

                    //If enitity 1 dosent have ballofspikePUPcomponent loose life
                    if (bspc1 == null)
                    {
                        HealthComponent hc1 = ComponentManager.Instance.GetEntityComponent<HealthComponent>(ent1);
                        //hc1.health -= 1;
                        pcp1.isFalling = true;
                    }
                    //If enitity 2 dosent have ballofspikePUPcomponent loose life
                    if (bspc2 == null)
                    {
                        HealthComponent hc2 = ComponentManager.Instance.GetEntityComponent<HealthComponent>(ent2);
                        //hc2.health -= 1;
                        pcp2.isFalling = true;
                    }
                    pushAway(ent1, ent2, gt);
                }
            }
        }

        /// <summary>
        /// A method for handling Player versus PowerUp collision
        /// </summary>
        /// <param name="ent1"> ID of entity 1 </param>
        /// <param name="ent2"> ID of entity 2 </param>
        private void PlayerVSPowerup(int ent1, int ent2)
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
            PlayerComponent playerComp = ComponentManager.Instance.GetEntityComponent<PlayerComponent>(player);
            if (!playerComp.isFalling)
            {
                PowerUppComponent tes = ComponentManager.Instance.GetEntityComponent<PowerUppComponent>(power);
                ComponentManager.Instance.AddComponentToEntity(player, new SoundEffectComponent("powerup"));
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
            }
        }


        /// <summary>
        /// Removes Powerup from component manager
        /// </summary>
        /// <param name="tmep1"></param>
        /// <param name="temp2"></param>
        private void rec(int temp1, int temp2)
        {
            List<IComponent> list1 = ComponentManager.Instance.GetAllEntityComponents(temp1);
            List<IComponent> list2 = ComponentManager.Instance.GetAllEntityComponents(temp2);
            if (list1.OfType<PlayerComponent>().Any())
            {
                ComponentManager.Instance.RecycleID(temp2);
                ComponentManager.Instance.RemoveEntity(temp2);
            }
            else
            {
                ComponentManager.Instance.RecycleID(temp1);
                ComponentManager.Instance.RemoveEntity(temp1);
            }
        }
    }
}
