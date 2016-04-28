using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Source.Components
{

    class PlayerComponent
    {
        private string playerName;
        private int playerNumber;

        public PlayerComponent(string playerName)
        {
            this.playerName = playerName;
        }
        public PlayerComponent(int playerNumber)
        {
            playerName = "Player" + playerNumber;
        }
        public PlayerComponent()
        {
            playerName = "Player" + new Random().Next(1, 999999999);
        }
    }
}
