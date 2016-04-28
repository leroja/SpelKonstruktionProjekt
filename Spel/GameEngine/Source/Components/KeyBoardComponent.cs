using GameEngine.Source.Enumerator;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Source.Components
{
    public class KeyBoardComponent : IComponent
    {
        
        public Dictionary<string, Keys> keyBoardActions { get; set; }
        public Dictionary<string, ButtonStates> state { get; set; }

        public KeyBoardComponent()
        {
            keyBoardActions = new Dictionary<string, Keys>();
            state = new Dictionary<string, ButtonStates>();
        }

    }
}
