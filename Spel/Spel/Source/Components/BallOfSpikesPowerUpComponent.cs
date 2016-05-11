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
        public double time;
        public BallOfSpikesPowerUpComponent(double time)
        {
            SpikeTexture = Game.Inst().GetContent<Texture2D>("pic/Giant_spike_ball");
            this.time = time;
        }
    }
}
