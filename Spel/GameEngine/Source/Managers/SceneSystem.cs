using GameEngine.Source.Systems.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GameEngine.Source.Managers
{
    /// <summary>
    /// SceneManager class is responsible for handling the different scenes(states) that's available in the game.
    /// This is a singleton
    /// </summary>
    public class SceneSystem:IUpdate
    {
        private static SceneSystem instance;
        private IGamescene activeScene;

        /// <summary>
        /// SceneManager default constructor
        /// </summary>
        private SceneSystem()
        {

        }

        /// <summary>
        /// SceneManager singleton 
        /// </summary>
        public static SceneSystem Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SceneSystem();
                }
                return instance;
            }
        }

        /// <summary>
        /// setCurrentScene method is used for changing the variable of the current state enable to 
        /// keep track of it durring the gameplay
        /// </summary>
        /// <param name="currentState">this variable is a IGamestate object which should be the parentobject of all state objects.</param>
        public void setCurrentScene(IGamescene currentState)
        {
            activeScene = currentState;
            activeScene.onSceneCreated();
        }

        /// <summary>
        /// update function for the scenesystem, runs the update function for the current active scene(state). 
        /// this makes the scene able to check for conditions whenever we need to change the scene(state).
        /// </summary>
        /// <param name="gameTime"></param>
        public void update(GameTime gameTime)
        {
            if(activeScene!= null)
            {
                activeScene.onSceneUpdate();
            }        
        }
    }
}
