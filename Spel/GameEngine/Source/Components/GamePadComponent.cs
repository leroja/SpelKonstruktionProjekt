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
        public Dictionary<string, Buttons> gamepadActions { get; set; }
        public Dictionary<string, ButtonStates> gamepadStates{get; set;}
        public GamePadComponent(PlayerIndex playerIndex)
        {
            this.playerIndex = playerIndex;
            gamepadActions = new Dictionary<string, Buttons>();
            gamepadStates = new Dictionary<string, ButtonStates>();
        }
    }
}
