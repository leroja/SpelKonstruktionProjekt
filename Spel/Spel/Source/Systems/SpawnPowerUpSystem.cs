using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using GameEngine.Source.Systems.Interfaces;
using GameEngine.Source.Managers;
using GameEngine.Source.Components;
using Spel.Source.Components;

namespace Spel.Source.Systems
{
    /// <summary>
    /// System for Spawning new PowerUpps
    /// </summary>
    class SpawnPowerUpSystem : IUpdate
    {
        private double time;
        private double start;
        public SpawnPowerUpSystem(int times)
        {
            time = times;
            start = times;
        }
        /// <summary>
        /// checks if the lifetime on an powerupp is up. then removes old powerup and spawns a new one.
        /// </summary>
        /// <param name="gameTime"></param>
        public void update(GameTime gameTime)
        {

            time = time - gameTime.ElapsedGameTime.TotalSeconds;
            if (time <= 0)
            {
                List<int> list1 = ComponentManager.Instance.GetAllEntitiesWithComponentType<PowerUppComponent>();
                if (list1 != null)
                {
                    foreach (var id in list1)
                    {
                        IList<IComponent> temp2 = ComponentManager.Instance.GetAllEntityComponents(id);
                        if (temp2.OfType<PowerUppComponent>().Any() && !temp2.OfType<PlayerComponent>().Any())
                        {
                            ComponentManager.Instance.RemoveEntity(id);
                            ComponentManager.Instance.RecycleID(id);
                        }
                    }
                }
                Random temp = new Random();
                int x = temp.Next(0, Game.Instance.GraphicsDevice.Viewport.Width);
                int y = temp.Next(0, Game.Instance.GraphicsDevice.Viewport.Height);
                Vector2 pos = new Vector2(x, y);
                GameEntityFactory.Instance.CreateTestPowerUp(pos);
                GameEntityFactory.Instance.CreateTestPowerUp(new Vector2(temp.Next(0,Game.Instance.GraphicsDevice.Viewport.Width),temp.Next(0,Game.Instance.GraphicsDevice.Viewport.Height)));
                time = start;

            }
            
        }
    }
}
        