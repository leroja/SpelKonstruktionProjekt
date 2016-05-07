using GameEngine.Source.Systems.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spel.Source.Gamestates
{
    class EndingState : IGamestate
    {
        public List<int> entetiesInState { get; set; }

        /// <summary>
        /// endingState constructor is responsible for managing the enteties which is used durring the 
        /// ending state of the gameplay, when someone is pronunced the winner
        /// </summary>
        public EndingState()
        {
            /// some background, text and some presentation of the winner of the game.
        }
        public void onSceneCreated()
        {

        }
        public void onSceneUpdate()
        {

        }
    }
}
