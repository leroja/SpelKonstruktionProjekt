using GameEngine.Source.Systems.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameEngine.Source.Managers;
using GameEngine.Source.Components;

namespace GameEngine.Source.Systems
{
    public class WindowTitleFPSSystem : IDraw
    {
        private Game game;
        private float timeSinceLastUpdate = 0.0f;

        public void draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            List<int> ents = ComponentManager.Instance.GetAllEntitiesWithComponentType<FPSCounterComponent>();
            if (ents != null)
            {
                foreach (var ent in ents)
                {
                    FPSCounterComponent fps = ComponentManager.Instance.GetEntityComponent<FPSCounterComponent>(ent);

                    float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;


                    fps.framecount++;
                    timeSinceLastUpdate += elapsed;
                    if (timeSinceLastUpdate > 1)
                    {
                        fps.frameCounter = fps.framecount / timeSinceLastUpdate;


                        game.Window.Title = "FPS: " + fps.frameCounter;

                        fps.framecount = 0;
                        timeSinceLastUpdate -= 1;
                    }
                }
            }
        }
        public WindowTitleFPSSystem(Game g)
        {
            game = g;
        }
        
        
    }
}
