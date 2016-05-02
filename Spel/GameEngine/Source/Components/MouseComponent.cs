using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using GameEngine.Source.Enumerator;

namespace GameEngine.Source.Components
{
    public class MouseComponent : IComponent
    {
        public Dictionary<string, Buttons> mouseAction { get; set; }
        public Dictionary<string, ButtonStates> mouseActionState { get; set; }

        public MouseComponent()
        {
            mouseAction = new Dictionary<string, Buttons>();
            mouseActionState = new Dictionary<string, ButtonStates>();
           
        //}
    }
}
