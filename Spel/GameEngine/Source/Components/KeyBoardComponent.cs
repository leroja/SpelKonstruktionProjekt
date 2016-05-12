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
        
        public Dictionary<ActionsEnum, Keys> keyBoardActions { get; set; }
        public Dictionary<ActionsEnum, ButtonStates> state { get; set; }

        public KeyBoardComponent()
        {
            keyBoardActions = new Dictionary<ActionsEnum, Keys>();
            state = new Dictionary<ActionsEnum, ButtonStates>();
        }

        //@todo maybe add accessor function for geting & seting  keyBoardActions & states
        // and maybe something for removal of a keyboard action

    }
}
