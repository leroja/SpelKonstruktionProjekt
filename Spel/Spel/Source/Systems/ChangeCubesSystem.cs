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

namespace Spel.Source.Systems
{
    public class ChangeCubesSystem : IUpdate
    {

        private List<int> entities;
        private Random rand;
        private int width;
        private int height;

        public ChangeCubesSystem()
        {
            entities = new List<int>();

            rand = new Random();
            width = Game.Instance.GraphicsDevice.Viewport.Width;
            height = Game.Instance.GraphicsDevice.Viewport.Height;

            for (int i = 0; i < 4; i++)
            {
                int x = rand.Next(0, width);
                int y = rand.Next(0, height);
                Vector2 pos = new Vector2(x, y);

                GameEntityFactory.Instance.CreateChangeCube(pos, "pic/changedir", 30, 30);
            }
        }

        public void update(GameTime gameTime)
        {
            width = Game.Instance.GraphicsDevice.Viewport.Width;
            height = Game.Instance.GraphicsDevice.Viewport.Height;
            Dictionary<int, IComponent> dic = ComponentManager.Instance.GetAllEntitiesAndComponentsWithComponentType<ChangeCubeComponent>();

            if(dic != null)
            {
                foreach (var item in dic)
                {
                    ChangeCubeComponent change = (ChangeCubeComponent)item.Value;
                    
                    change.time += (float)gameTime.ElapsedGameTime.TotalSeconds;

                    if (change.isTaken || change.time > 5)
                    {
                        PositionComponent pos = ComponentManager.Instance.GetEntityComponent<PositionComponent>(item.Key);
                        pos.position.X = rand.Next(0, width);
                        pos.position.Y = rand.Next(0, height);
                        change.isTaken = false;
                        change.time = 0;
                    }                    
                }
            }
        }
    }
}
