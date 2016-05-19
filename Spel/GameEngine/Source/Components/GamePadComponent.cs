using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using GameEngine.Source.Enumerator;

namespace GameEngine.Source.Components
{
    /// <summary>
    /// GamePadComponent stores data for playerIndex, possible actions, gamepadStates.
    /// </summary>
    public class GamePadComponent : IComponent
    {
        public PlayerIndex playerIndex;
        public Dictionary<ActionsEnum, Buttons> gamepadActions { get; set; }
        public Dictionary<ActionsEnum, ButtonStates> gamepadStates{get; set;}
        public GamePadComponent(PlayerIndex playerIndex)
        {
            this.playerIndex = playerIndex;
            gamepadActions = new Dictionary<ActionsEnum, Buttons>();
            gamepadStates = new Dictionary<ActionsEnum, ButtonStates>();
        }
    }
}
