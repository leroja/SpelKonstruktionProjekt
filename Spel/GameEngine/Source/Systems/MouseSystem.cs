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
    public class MouseSystem : IInput
    {
        public MouseState prevState { get; set; }

        public MouseState curState { get; set; }
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
        private void UpdateStates()
        {
            prevState = curState;
            curState = Mouse.GetState();
        }

        public void UpdateActionStates(MouseComponent mouseComponent)
        {

            updateBtn(mouseComponent, curState.RightButton, prevState.RightButton, "RightButton");
            updateBtn(mouseComponent, curState.LeftButton, prevState.RightButton, "LeftButton");
            updateBtn(mouseComponent, curState.MiddleButton, prevState.RightButton, "MiddleButton");
        }

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
