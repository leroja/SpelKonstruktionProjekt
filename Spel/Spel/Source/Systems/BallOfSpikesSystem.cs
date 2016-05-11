﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spel.Source.Components;
using GameEngine.Source.Systems.Interfaces;
using GameEngine.Source.Managers;
using GameEngine.Source.Components;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Spel.Source.Systems
{
    class BallOfSpikesSystem : IPowerupSystem
    {
        GameTime time;
        public void OnPowerUpColision()
        {
            throw new NotImplementedException();
        }

        public void OnPowerUpPicup(int id)
        {
            ComponentManager test = ComponentManager.Instance; 
            BallOfSpikesPowerUpComponent ball = new BallOfSpikesPowerUpComponent(10);
            DrawableComponent newDraw = test.GetEntityComponent<DrawableComponent>(id);
            ball.prevTexture = newDraw.texture;
            newDraw.texture = ball.SpikeTexture;
            test.AddComponentToEntity(id, ball);


            CollisionRectangleComponent rec = ComponentManager.Instance.GetEntityComponent<CollisionRectangleComponent>(id);
            PositionComponent pos = ComponentManager.Instance.GetEntityComponent<PositionComponent>(id);
            rec.CollisionRec = new Rectangle((int)pos.position.X, (int)pos.position.Y, newDraw.texture.Width, newDraw.texture.Height);
            rec.CollisionRec.X = (int)pos.position.X;
            rec.CollisionRec.Y = (int)pos.position.Y;

            rec.CollisionRec.Width = newDraw.texture.Width;
            rec.CollisionRec.Height = newDraw.texture.Height;
        }

        public void update(GameTime gameTime)
        {
            time = gameTime;
            ControllPoweruppLife(gameTime);
        }
        public void ControllPoweruppLife(GameTime timer)
        {
            List<int> balls = ComponentManager.Instance.GetAllEntitiesWithComponentType<BallOfSpikesPowerUpComponent>();
            if (balls != null)
            {
                foreach (var ball in balls)
                {
                    BallOfSpikesPowerUpComponent b = ComponentManager.Instance.GetEntityComponent<BallOfSpikesPowerUpComponent>(ball);
                    double test = b.time - timer.ElapsedGameTime.TotalSeconds;
                    if (test <= 0)
                    {
                      
                        DrawableComponent newDraw = ComponentManager.Instance.GetEntityComponent<DrawableComponent>(ball);
                        BallOfSpikesPowerUpComponent pow = ComponentManager.Instance.GetEntityComponent<BallOfSpikesPowerUpComponent>(ball);
                        newDraw.texture = b.prevTexture;
                        ComponentManager.Instance.RemoveComponentFromEntity(ball, b);
                       
                    }
                    b.time = test;
                }
            }
        }
    }
}
