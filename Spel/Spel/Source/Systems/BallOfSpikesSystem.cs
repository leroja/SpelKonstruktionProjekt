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
            BallOfSpikesPowerUpComponent ball = new BallOfSpikesPowerUpComponent(Game.Inst().GetContent<Texture2D>("Pic/Giant_spike_ball"),time);
            DrawableComponent newDraw = test.GetEntityComponent<DrawableComponent>(id);
            ball.prevTexture = newDraw.texture;
            newDraw.texture = ball.SpikeTexture;
            test.AddComponentToEntity(id, ball);
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
                   double test = timer.TotalGameTime.TotalSeconds - b.Pickuptime.TotalGameTime.TotalSeconds;
                    if (test > 30)
                    {
                        ComponentManager.Instance.RemoveComponentFromEntity(ball, b);
                    }
                }
            }
        }
    }
}
