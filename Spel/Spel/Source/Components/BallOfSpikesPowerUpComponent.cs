using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using GameEngine.Source.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Spel.Source.Components
{
    public class BallOfSpikesPowerUpComponent : IComponent
    {
        public Texture2D SpikeTexture;
        public Texture2D prevTexture;
        public GameTime Pickuptime;
        public BallOfSpikesPowerUpComponent(Texture2D texture, GameTime time)
        {
            SpikeTexture = texture;
            Pickuptime = time;
        }
    }
}
