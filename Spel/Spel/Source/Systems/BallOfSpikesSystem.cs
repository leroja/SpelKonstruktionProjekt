using System;
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
    /// <summary>
    /// System for Controlling the BallOfSpikesPowerUp
    /// </summary>
    class BallOfSpikesSystem : IPowerupSystem
    {
        GameTime time;
        public void OnPowerUpColision()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Creates Power Upp component and adds it to an entity
        /// </summary>
        /// <param name="id"></param>
        public void OnPowerUpPicup(int id)
        {
            BallOfSpikesPowerUpComponent temp = ComponentManager.Instance.GetEntityComponent<BallOfSpikesPowerUpComponent>(id);
            if (temp == null)
            {
                ComponentManager test = ComponentManager.Instance;
                BallOfSpikesPowerUpComponent ball = new BallOfSpikesPowerUpComponent(10);
                DrawableComponent newDraw = test.GetEntityComponent<DrawableComponent>(id);
                AnimationComponent anima = test.GetEntityComponent<AnimationComponent>(id);
                ball.prevTexture = newDraw.texture;
                newDraw.texture = ball.SpikeTexture;
                if (anima != null)
                {
                    ball.anime = anima;
                    ComponentManager.Instance.RemoveComponentFromEntity(id, anima);
                }
                test.AddComponentToEntity(id, ball);


                CollisionRectangleComponent rec = ComponentManager.Instance.GetEntityComponent<CollisionRectangleComponent>(id);
                PositionComponent pos = ComponentManager.Instance.GetEntityComponent<PositionComponent>(id);
                rec.CollisionRec = new Rectangle((int)pos.position.X, (int)pos.position.Y, newDraw.texture.Width, newDraw.texture.Height);
                rec.CollisionRec.X = (int)pos.position.X;
                rec.CollisionRec.Y = (int)pos.position.Y;

                rec.CollisionRec.Width = newDraw.texture.Width;
                rec.CollisionRec.Height = newDraw.texture.Height;
            }
            else
            {
                temp.lifeTime += 10;
            }
        }
        /// <summary>
        /// checks if any SpikBall component on an entity is out of time.
        /// if out of time then removes the component
        /// </summary>
        /// <param name="gameTime"></param>
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
                    double test = b.lifeTime - timer.ElapsedGameTime.TotalSeconds;
                    if (test <= 0)
                    {
                      
                        DrawableComponent newDraw = ComponentManager.Instance.GetEntityComponent<DrawableComponent>(ball);
                        BallOfSpikesPowerUpComponent pow = ComponentManager.Instance.GetEntityComponent<BallOfSpikesPowerUpComponent>(ball);
                        newDraw.texture = b.prevTexture;
                        if (b.anime != null)
                        {
                            ComponentManager.Instance.AddComponentToEntity(ball, b.anime);
                        }
                        ComponentManager.Instance.RemoveComponentFromEntity(ball, b);
                       
                    }
                    b.lifeTime = test;
                }
            }
        }
    }
}
