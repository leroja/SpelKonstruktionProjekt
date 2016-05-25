using GameEngine.Source.Systems.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameEngine.Source.Components;
using GameEngine.Source.Managers;

namespace Spel.Source.Systems
{
    /// <summary>
    /// A system for drawing the time left of the ttl components
    /// </summary>
    public class DrawTTLSystem : IDraw
    {
        private SpriteFont font;

        public void draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Dictionary<int, IComponent> dic = ComponentManager.Instance.GetAllEntitiesAndComponentsWithComponentType<TTLComponent>();
            if(dic != null)
            {
                foreach (var item in dic)
                {
                    TTLComponent ttl = (TTLComponent)item.Value;
                    float timeLeft = ttl.maxTime - ttl.curTime;

                    PositionComponent pos = ComponentManager.Instance.GetEntityComponent<PositionComponent>(item.Key);

                    Vector2 offset = new Vector2(10, -30);
                    Vector2 position = pos.position + offset;
                    
                    spriteBatch.DrawString(font, timeLeft+"", position, Color.Black);
                }
            }
        }
        public DrawTTLSystem(string FontName)
        {
            font = Game.Instance.GetContent<SpriteFont>(FontName);
        }
    }
}
