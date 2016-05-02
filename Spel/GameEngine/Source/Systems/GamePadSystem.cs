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
    /// GamePadSystem handles the input from gamepadComponents for specific PlayerIndexes.
    /// </summary>
    public class GamePadSystem : IInput
    {
        public GamePadState [] prevState { get; set; }
        public GamePadState [] curState { get; set; }

        public GamePadSystem()
        {
            prevState = new GamePadState[4];
            curState = new GamePadState[4];
        }

        /// <summary>
        /// update gets all gamepadComponent and loops thru all relvant components which is passed to UpdateActionStates.
        /// </summary>
        /// <param name="gameTime">Monogame specific variable for game time</param>
        public void update(GameTime gameTime)
        {
            updateStates();

            List<int> entities = ComponentManager.Instance.GetAllEntitiesWithComponentType<GamePadComponent>();
            if (entities != null)
            {
                foreach (var item in entities)
                {
                    GamePadComponent gamepadComponent = ComponentManager.Instance.GetEntityComponent<GamePadComponent>(item);
                    UpdateActionStates(gamepadComponent);
                }
            }
        }

        /// <summary>
        /// updateStats handles the update of currentstate by looping thru gamepad states.
        /// </summary>
        private void updateStates()
        {
            for (int i = 0; i < curState.Length; i++)
            {
                prevState[i] = curState[i];
                curState[i] = GamePad.GetState((PlayerIndex)i);
            }
        }

        /// <summary>
        /// UpdateActionStates loops thru gamepadActions for a specific gamepadComponenet and applies appropriet action relative to buttonstate.
        /// </summary>
        /// <param name="gamepadComponent"></param>
        public void UpdateActionStates(GamePadComponent gamepadComponent)
        {
            foreach (string action in gamepadComponent.gamepadActions.Keys)
            {
                Buttons button = gamepadComponent.gamepadActions[action];
                bool presentState = curState[(int)gamepadComponent.playerIndex].IsButtonDown(button);
                bool oldState = curState[(int)gamepadComponent.playerIndex].IsButtonDown(button);

                if(presentState && !oldState)
                {
                    gamepadComponent.gamepadStates[action] = ButtonStates.Pressed;
                }
                else if (presentState && oldState)
                {
                    gamepadComponent.gamepadStates[action] = ButtonStates.Hold;
                }
                else if (!presentState && oldState)
                {
                    gamepadComponent.gamepadStates[action] = ButtonStates.Released;
                }
                else
                {
                    gamepadComponent.gamepadStates[action] = ButtonStates.Not_Pressed;
                }
            }
        }
    }
}
