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
        public Dictionary<string, ButtonStates> mouseActionState { get; set; }

        public MouseComponent()
        {
            mouseActionState = new Dictionary<string, ButtonStates>();

            mouseActionState.Add("LeftButton", ButtonStates.Not_Pressed);
            mouseActionState.Add("MiddleButton", ButtonStates.Not_Pressed);
            mouseActionState.Add("RightButton", ButtonStates.Not_Pressed);
        }
    }
}
