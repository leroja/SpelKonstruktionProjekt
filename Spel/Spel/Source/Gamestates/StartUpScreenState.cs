using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Source.Systems.Interfaces;

namespace Spel.Source.Gamestates
{
    class StartUpScreenState : IGamestate
    {
        public List<int> entetiesInState { get; set; }

        /// <summary>
        /// StartUpState constructor, is responsible for setting the scene for the startup state of the gameplay
        /// </summary>
        public StartUpScreenState()
        {
            //This function should create the needed enteties for the start up screen exemple add background, texture component, maybe add some text on the startup screen 
            //Create some help function if that is needed 
            //Maybe splash screen of some sort
        }

    }
}
