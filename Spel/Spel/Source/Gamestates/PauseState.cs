using GameEngine.Source.Systems.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spel.Source.Gamestates
{
    class PauseState : IGamestate
    {
        public List<int> entetiesInState { get; set; }

        /// <summary>
        /// PauseState constructor, is responisble for containing the enteties which are active and displayed in
        /// the paused state of the gameplay.
        /// </summary>
        public PauseState()
        {
            //This constructor should add the enteties which is special for the pause state of the gameplay, maybe some text and/or 
            //different background. (Control for unpausing (?)) 
        }
        public void onSceneCreated()
        {

        }
        public void onSceneUpdate()
        {

        }
    }
}
