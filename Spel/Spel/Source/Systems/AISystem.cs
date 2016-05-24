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

                    //AI(gameTime, id);

                    if(pos.position.Y + 30 > Game.Instance.GraphicsDevice.Viewport.Height/2 && !pc.isFalling)
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

            }
        }








        private void AI(GameTime gameTime, int AIid)
        {
            PositionComponent pos = ComponentManager.Instance.GetEntityComponent<PositionComponent>(AIid);
            VelocityComponent vel = ComponentManager.Instance.GetEntityComponent<VelocityComponent>(AIid);
            DirectionComponent dir = ComponentManager.Instance.GetEntityComponent<DirectionComponent>(AIid);
            JumpComponent jump = ComponentManager.Instance.GetEntityComponent<JumpComponent>(AIid);
            PlayerComponent pc = ComponentManager.Instance.GetEntityComponent<PlayerComponent>(AIid);
            DirectionComponent dc = ComponentManager.Instance.GetEntityComponent<DirectionComponent>(AIid);


            Dictionary<int, IComponent> platforms = ComponentManager.Instance.GetAllEntitiesAndComponentsWithComponentType<PlatformComponent>();
            Dictionary<int, IComponent> players = ComponentManager.Instance.GetAllEntitiesAndComponentsWithComponentType<PlayerComponent>();
            Dictionary<int, IComponent> powerUps = ComponentManager.Instance.GetAllEntitiesAndComponentsWithComponentType<PowerUppComponent>();

            float nearestPlayer = 0, nearestPlatform = 0, dist = float.MaxValue, nearestPowerUp = 0;
            
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



        }
    }
}
