using GameEngine.Source.Systems.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using GameEngine.Source.Managers;
using GameEngine.Source.Components;

namespace GameEngine.Source.Systems
{
    class SoundEffectSystem : IUpdate
    {
        public void update(GameTime gameTime)
        {
            List<int> ents = ComponentManager.Instance.GetAllEntitiesWithComponentType<SoundEffectComponent>();
            if (ents != null)
            {
                foreach (var ent in ents)
                {
                    SoundEffectComponent sec = ComponentManager.Instance.GetEntityComponent<SoundEffectComponent>(ent);
                    AudioManager.Instance.PlaySoundEffect(sec.soundEffectName);
                    ComponentManager.Instance.RemoveComponentFromEntity(ent, sec);
                }
            }
        }
    }
}
