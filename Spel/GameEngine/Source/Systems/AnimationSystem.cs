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
    class AnimationSystem : IUpdate
    {
        public void update(GameTime gameTime)
        {
            List<int> enteties = ComponentManager.Instance.GetAllEntitiesWithComponentType<AnimationComponent>();

            if(enteties != null)
            {
                foreach (int entity in enteties)
                {
                    AnimationComponent anim = ComponentManager.Instance.GetEntityComponent<AnimationComponent>(entity);


                    anim.timeElapsedSinceLastFrame += gameTime.ElapsedGameTime.TotalSeconds;

                    if(anim.timePerFrame < anim.timeElapsedSinceLastFrame)
                    {
                        anim.timeElapsedSinceLastFrame = 0;
                        anim.currentFrame++;

                        if(anim.currentFrame > anim.getAnimationLength())
                        {
                            anim.currentFrame = 0;
                        }
                        anim.setNewPosRectangle(anim.currentFrame);
                    }
                }
            }
            
        }
    }
}
