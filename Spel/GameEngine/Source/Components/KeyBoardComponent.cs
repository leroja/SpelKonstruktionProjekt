using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Source.Components
{
    class KeyBoardComponent : IComponent
    {

        //public List<string> actions { get; set; }
        public Dictionary<string, Keys> keyBoardActions { get; set; }
        // inte klar
        public Dictionary<string, string> state { get; set; }

        public KeyBoardComponent()
        {
            keyBoardActions = new Dictionary<string, Keys>();
            state = new Dictionary<string, string>();
        }

        //public KeyBoardComponent(List<string> actions)
        //{
        //    this.actions = actions;
        //}

        //public KeyBoardComponent(string action, Keys key)
        //{
        //    this.keyBoardActions = new Dictionary<string, Keys>();
        //    keyBoardActions.Add(action, key);
        //}


        public string GetActionState(string action)
        {
            if (state.ContainsKey(action))
            {
                return state[action];
            }
            return null;
        }

        //public void SetAction(string action, string state)
        //{
        //    this.state[action] = state;
        //}

    }
}
