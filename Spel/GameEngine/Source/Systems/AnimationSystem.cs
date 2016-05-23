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
    /// AnimationSystem class this class is responsible for handling the updates for the 
    /// animation components.
    /// </summary>
    class AnimationSystem : IUpdate
    {
        /// <summary>
        /// update function, is used for handling the update process for the animations.
        /// </summary>
        /// <param name="gameTime">takes a GameTime object as a parameter inorder to check if it should change the current frame</param>
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

                        if(anim.currentFrame > anim.getAnimationLength() && anim.oneTime == false)
                        {
                            anim.currentFrame = 0;
                        }
                        else if (anim.currentFrame > anim.getAnimationLength() && anim.oneTime == true)
                        {
                            anim.currentFrame = anim.getAnimationLength();
                        }
                        anim.setNewPosRectangle(anim.currentFrame);
                    }
                }
            }
            
        }
    }
}
