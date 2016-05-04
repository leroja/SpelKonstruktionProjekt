﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Source.Components
{
    /// <summary>
    /// PlayerComponent class is added to the enteties which is representations
    /// of acctuall human players.
    /// </summary>
    public class PlayerComponent : IComponent
    {
        public string playerName { get; set; }

        /// <summary>
        /// PlayerComponent constructor
        /// </summary>
        /// <param name="playerName">Takes a string which will be the name of the player</param>
        public PlayerComponent(string playerName)
        {
            this.playerName = playerName;
        }
        /// <summary>
        /// PlayerComponent constructor
        /// </summary>
        /// <param name="playerNumber">Takes an id which will be represented as the players name. 
        /// For example if you create a player component with id 1. The players name will be "Player1".</param>
        public PlayerComponent(int playerNumber)
        {
            playerName = "Player" + playerNumber;
        }
        /// <summary>
        /// PlayerComponent constructor, can be used when you're lazy and the names of the players does not matter.
        /// Will give the player names ending with a random number, for example "Player1337".
        /// </summary>
        public PlayerComponent()
        {
            playerName = "Player" + new Random().Next(1, 999999999);
        }
    }
}
