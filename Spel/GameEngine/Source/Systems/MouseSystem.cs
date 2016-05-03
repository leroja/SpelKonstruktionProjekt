using GameEngine.Source.Systems.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using GameEngine.Source.Managers;
using GameEngine.Source.Components;
using GameEngine.Source.Enumerator;

namespace GameEngine.Source.Systems
{
    /// <summary>
    /// 
    /// </summary>
    public class MouseSystem : IInput
    {
        public MouseState prevState { get; set; }
        public MouseState curState { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        public void update(GameTime gameTime)
        {
            UpdateStates();

            List<int> entities = ComponentManager.Instance.GetAllEntitiesWithComponentType<MouseComponent>();

            if (entities != null)
            {
                foreach (var item in entities)
                {
                    MouseComponent mouse = ComponentManager.Instance.GetEntityComponent<MouseComponent>(item);
                    UpdateActionStates(mouse);
                }
            }
        }
        /// <summary>
        /// updates the previous & current State of the Mouse
        /// </summary>
        private void UpdateStates()
        {
            prevState = curState;
            curState = Mouse.GetState();
        }

        /// <summary>
        /// updates the state of left, right & middle click
        /// </summary>
        /// <param name="mouseComponent"></param>
        public void UpdateActionStates(MouseComponent mouseComponent)
        {

            updateBtn(mouseComponent, curState.RightButton, prevState.RightButton, "RightButton");
            updateBtn(mouseComponent, curState.LeftButton, prevState.RightButton, "LeftButton");
            updateBtn(mouseComponent, curState.MiddleButton, prevState.RightButton, "MiddleButton");
        }

        /// <summary>
        /// updates state of a specific mousebutton
        /// </summary>
        /// <param name="mouse"></param>
        /// <param name="curState"></param>
        /// <param name="prevState"></param>
        /// <param name="button"></param>
        private void updateBtn(MouseComponent mouse, ButtonState curState, ButtonState prevState, string button)
        {
            if (curState == ButtonState.Pressed && prevState != ButtonState.Pressed)
            {
                mouse.mouseActionState[button] = ButtonStates.Pressed;
            }
            else if (curState == ButtonState.Pressed && prevState == ButtonState.Pressed)
            {
                mouse.mouseActionState[button] = ButtonStates.Hold;
            }
            else if (curState != ButtonState.Pressed && prevState == ButtonState.Pressed)
            {
                mouse.mouseActionState[button] = ButtonStates.Released;
            }
            else
            {
                mouse.mouseActionState[button] = ButtonStates.Not_Pressed;
            }
        }
    }
}
