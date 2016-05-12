using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Source.Components
{
    /// <summary>
    /// a component that contains the name of a SoundEffect that is to be played
    /// </summary>
    public class SoundEffectComponent : IComponent
    {
        public string soundEffectName { get; set; }

        public SoundEffectComponent(string soundEffectName)
        {
            this.soundEffectName = soundEffectName;
        }
    }
}
