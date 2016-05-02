using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Source.Components
{
    public class SoundEffectComponent : IComponent
    {
        public string soundEffectName { get; set; }

        public SoundEffectComponent(string soundEffectName)
        {
            this.soundEffectName = soundEffectName;
        }
    }
}
