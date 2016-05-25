using GameEngine.Source.Systems.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using GameEngine.Source.Components;
using GameEngine.Source.Managers;
using Spel.Source.Components;
using Spel.Source.Enum;

namespace Spel.Source.Systems
{
    class AISystem : IUpdate
    {
        private const float gravity = 500F;
        private const float sideMovement = 200F;
        private int nearestPlayer = 0, nearestPlatform = 0, nearestPowerUp = 0;


        public void update(GameTime gameTime)
        {
            Dictionary<int, IComponent> dic = ComponentManager.Instance.GetAllEntitiesAndComponentsWithComponentType<AIComponent>();

            if(dic != null)
            {
                foreach (var item in dic)
                {
                    int id = item.Key;

                    PositionComponent pos = ComponentManager.Instance.GetEntityComponent<PositionComponent>(id);
                    VelocityComponent vel = ComponentManager.Instance.GetEntityComponent<VelocityComponent>(id);
                    DirectionComponent dir = ComponentManager.Instance.GetEntityComponent<DirectionComponent>(id);
                    JumpComponent jump = ComponentManager.Instance.GetEntityComponent<JumpComponent>(id);
                    PlayerComponent pc = ComponentManager.Instance.GetEntityComponent<PlayerComponent>(id);
                    DirectionComponent dc = ComponentManager.Instance.GetEntityComponent<DirectionComponent>(id);

                    try
                    {
                        if (AI(gameTime, id))
                        {
                            if (dc.directio == Direction.Still)
                            {
                                dc.directio = dc.preDir;
                            }
                            if (vel.velocity.Y > -jump.maxJumpHeight)
                            {
                                vel.velocity.Y -= jump.jumpHeight;
                                ComponentManager.Instance.AddComponentToEntity(id, new SoundEffectComponent("jump"));
                            }
                        }
                        vel.velocity.Y += gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        pos.position += vel.velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                    catch (Exception)
                    {

                        
                    }
                }
            }
        }







        /// <summary>
        /// a method that determines if the AI shall jump
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="AIid"> Id of AI Player </param>
        /// <returns> True if the AI shall jump, false if not </returns>
        private bool AI(GameTime gameTime, int AIid)
        {
            PositionComponent pos = ComponentManager.Instance.GetEntityComponent<PositionComponent>(AIid);
            DrawableComponent drawComp = ComponentManager.Instance.GetEntityComponent<DrawableComponent>(AIid);
            VelocityComponent vel = ComponentManager.Instance.GetEntityComponent<VelocityComponent>(AIid);
            DirectionComponent dir = ComponentManager.Instance.GetEntityComponent<DirectionComponent>(AIid);
            JumpComponent jump = ComponentManager.Instance.GetEntityComponent<JumpComponent>(AIid);
            PlayerComponent pc = ComponentManager.Instance.GetEntityComponent<PlayerComponent>(AIid);
            DirectionComponent dc = ComponentManager.Instance.GetEntityComponent<DirectionComponent>(AIid);


            if (pc.isFalling)
            {
                return false;
            }

            Dictionary<int, IComponent> platforms = ComponentManager.Instance.GetAllEntitiesAndComponentsWithComponentType<PlatformComponent>();
            Dictionary<int, IComponent> players = ComponentManager.Instance.GetAllEntitiesAndComponentsWithComponentType<PlayerComponent>();
            Dictionary<int, IComponent> powerUps = ComponentManager.Instance.GetAllEntitiesAndComponentsWithComponentType<PowerUppComponent>();

            float dist = float.MaxValue;
            
            foreach (var item in platforms)
            {
                int id = item.Key;
                PositionComponent platPos = ComponentManager.Instance.GetEntityComponent<PositionComponent>(id);

                var distance = Vector2.DistanceSquared(pos.position, platPos.position);
                if (nearestPlatform == 0 || distance < dist)
                {
                    nearestPlatform = id;
                    dist = distance;
                }
            }
            dist = float.MaxValue;
            foreach (var item in players)
            {
                int id = item.Key;

                PositionComponent playPos = ComponentManager.Instance.GetEntityComponent<PositionComponent>(id);

                var distance = Vector2.DistanceSquared(pos.position, playPos.position);
                if (nearestPlayer == 0 || distance < dist)
                {
                    nearestPlayer = id;
                    dist = distance;
                }
            }

            dist = float.MaxValue;
            if(powerUps != null) {
                foreach (var item in powerUps)
                {
                    int id = item.Key;

                    PositionComponent powerPos = ComponentManager.Instance.GetEntityComponent<PositionComponent>(id);

                    var distance = Vector2.DistanceSquared(pos.position, powerPos.position);
                    if (nearestPowerUp == 0 || distance < dist)
                    {
                        nearestPowerUp = id;
                        dist = distance;
                    }
                }
            }

            float distToBottom = Game.Instance.GraphicsDevice.Viewport.Height - pos.position.Y;
            float distToTop = pos.position.Y;

            if(nearestPlatform != 0)
            {
                PositionComponent platPos = ComponentManager.Instance.GetEntityComponent<PositionComponent>(nearestPlatform);
                DrawableComponent draw = ComponentManager.Instance.GetEntityComponent<DrawableComponent>(nearestPlatform);
                int width, height;

                if (ComponentManager.Instance.CheckIfEntityHasComponent<AnimationComponent>(AIid))
                {
                    AnimationComponent ani = ComponentManager.Instance.GetEntityComponent<AnimationComponent>(AIid);
                    width = ani.sourceRectangle.Width;
                    height = ani.sourceRectangle.Height;
                }
                else
                {
                    width = drawComp.texture.Width;
                    height = drawComp.texture.Height;
                }


                //float dis = Vector2.Distance(pos.position, platPos.position);
                //Vector2 d = pos.position - platPos.position;
                //Vector2 d = platPos.position - pos.position;

                


            }

            if(pos.position.Y + 30 > Game.Instance.GraphicsDevice.Viewport.Height / 2)
            {
                return true;
            }

            return false;

        }
    }
}
