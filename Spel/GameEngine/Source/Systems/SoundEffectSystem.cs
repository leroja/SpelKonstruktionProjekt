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
    /// <summary>
    /// 
    /// </summary>
    class SoundEffectSystem : IUpdate
    {
        public void update(GameTime gameTime)
        {
            List<int> ents = ComponentManager.Instance.GetAllEntitiesWithComponentType<SoundEffectComponent>();
            if (ents != null)
            {
                foreach (var ent in ents)
                {
                    float pan, pitch;
                    int width = 1190; //@todo find a better way of getting the width of the gamescreen
                    PositionComponent pos = ComponentManager.Instance.GetEntityComponent<PositionComponent>(ent);
                    if(pos.position.X > width * 0.66f)
                    {
                        pan = 1;
                        pitch = 1;
                    }else if(pos.position.X < width * 0.33f)
                    {
                        pan = -1;
                        pitch = -1;
                    }
                    else
                    {
                        pitch = 0;
                        pan = 0;
                    }

                    SoundEffectComponent sec = ComponentManager.Instance.GetEntityComponent<SoundEffectComponent>(ent);
                    AudioManager.Instance.PlaySoundEffect(sec.soundEffectName, pan, pitch);
                    ComponentManager.Instance.RemoveComponentFromEntity(ent, sec);
                }
            }
        }
    }
}
