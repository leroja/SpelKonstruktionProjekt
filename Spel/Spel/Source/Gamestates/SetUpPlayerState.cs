using GameEngine.Source.Systems.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spel.Source.Gamestates
{
    class SetUpPlayerState : IGamestate
    {
        public List<int> entetiesInState { get; set; }

        /// <summary>
        /// SetUpPlayerState constructor, which is responsible for adding enteties to the scene in the gameplay where the players choose their caracters and
        /// controlls etc.
        /// </summary>
        public SetUpPlayerState()
        {
            //add the enteties which should be displayed on the screen when the players choose their caracters. Player enteties is to be created after leaving this state therefore the 
            //add entetiestolist-function needs to be called before entering the playing-state
        }
    }
}
