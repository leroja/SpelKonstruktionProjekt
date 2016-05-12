using GameEngine.Source.Systems.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spel.Source.Gamestates
{
    /// <summary>
    /// PauseScene class is the class responisble for handle the paused state of the gameplay.
    /// </summary>
    class PauseScene : IGamescene
    {
        public List<int> entitiesInState { get; set; }

        /// <summary>
        /// PauseScene constructor, is responisble for containing the enteties which are active and displayed in
        /// the paused state of the gameplay.
        /// </summary>
        public PauseScene()
        {
            //This constructor should add the enteties which is special for the pause state of the gameplay, maybe some text and/or 
            //different background. (Control for unpausing (?)) 
        }
        /// <summary>
        /// onSceneCreated this function is called whenever the current gamestate is changed. This function should contain logic that 
        /// needs to be processed before the state is shown for the player. This could be enteties that's not able to be created pre-runtime.
        /// </summary>
        public void onSceneCreated()
        {

        }
        /// <summary>
        /// onSceneUpdate this function handles the logic for the state which should be run durring the update partion of the game.
        /// For example this could be to check for conditions to continue to the next state of the gameplay. 
        /// </summary>
        public void onSceneUpdate()
        {

        }
    }
}
