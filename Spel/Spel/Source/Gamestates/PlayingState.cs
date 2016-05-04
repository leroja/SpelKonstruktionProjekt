using GameEngine.Source.Systems.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spel.Source.Gamestates
{
    class PlayingState : IGamestate
    {
        public List<int> entetiesInState { get; set; }

        public PlayingState()
        {

        }
    }
}
