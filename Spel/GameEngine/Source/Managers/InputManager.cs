using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Source.Managers
{
    class InputManager
    {

        // Flytta till ett system istället?

        private static InputManager instance;

        Dictionary<string, Keys> keyBoardActions = new Dictionary<string, Keys>();
        Dictionary<PlayerIndex, Dictionary<string, Buttons>> gamePadActions = new Dictionary<PlayerIndex, Dictionary<string,Buttons>>();

        KeyboardState currentKeyBoardState = Keyboard.GetState();
        KeyboardState previousKeyBoardState = Keyboard.GetState();

        GamePadState[] currentGamePadState = new GamePadState[4];
        GamePadState[] previousGamePadState = new GamePadState[4];

        private InputManager()
        {
        }

        public static InputManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new InputManager();
                }
                return instance;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void update()
        {
            previousKeyBoardState = currentKeyBoardState;
            currentKeyBoardState = Keyboard.GetState();
            
            for (int i = 0; i < currentGamePadState.Length; i++)
            {
                previousGamePadState[i] = currentGamePadState[i];
                currentGamePadState[i] = GamePad.GetState((PlayerIndex)i);
            }
        }



        /// <summary>
        /// binds an action to a key on the keyboard
        /// </summary>
        /// <param name="action">
        /// name of the action
        /// </param>
        /// <param name="key">
        /// 
        /// </param>
        public void AddKeyBoardAction(string action, Keys key)
        {
            if (keyBoardActions.ContainsKey(action))
            {
                keyBoardActions.Remove(action);
            }
            keyBoardActions.Add(action, key);
        }

        /// <summary>
        /// removes the action and the associated key
        /// </summary>
        /// <param name="action">
        /// Name of the action
        /// </param>
        public void RemoveKeyBoardAction(string action)
        {
            if (keyBoardActions.ContainsKey(action))
            {
                keyBoardActions.Remove(action);
            }
        }

        /// <summary>
        /// checks if the key bound to the action has been pressed
        /// </summary>
        /// <param name="action">
        /// Name of the action
        /// </param>
        /// <returns>
        /// true if the key has been pressed
        /// fasle if it not has been pressed
        /// </returns>
        public bool KeyPressed(string action)
        {
            if (keyBoardActions.ContainsKey(action))
            {
                Keys key = keyBoardActions[action];
                if (currentKeyBoardState.IsKeyDown(key) && !previousKeyBoardState.IsKeyDown(key))
                {
                    return true;
                }
                
            }
            return false;
        }
        

        ////////////////

        /// <summary>
        /// binds an action to a gamepadbutton for the controller
        /// </summary>
        /// <param name="playerIndex">
        /// which controller
        /// </param>
        /// <param name="action">
        /// name of the action
        /// </param>
        /// <param name="button">
        /// 
        /// </param>
        public void AddGamePadAction(PlayerIndex playerIndex, string action, Buttons button)
        {
            if (!gamePadActions.ContainsKey(playerIndex))
            {
                gamePadActions[playerIndex] = new Dictionary<string, Buttons>();
            }


            if (gamePadActions[playerIndex].ContainsKey(action))
            {
                gamePadActions[playerIndex].Remove(action);
            }

            gamePadActions[playerIndex].Add(action, button);
        }


        /// <summary>
        /// removes the action and the associated gamepadbutton for the controller
        /// </summary>
        /// <param name="playerIndex">
        /// which controller
        /// </param>
        /// <param name="action">
        /// name of the action
        /// </param>
        public void RemoveGamePadAction(PlayerIndex playerIndex, string action)
        {
            if (gamePadActions.ContainsKey(playerIndex))
            {

                if (gamePadActions[playerIndex].ContainsKey(action))
                {
                    gamePadActions[playerIndex].Remove(action);
                }
            }
        }

        /// <summary>
        /// checks if the gamepadbutton bound to the action has been pressed
        /// </summary>
        /// <param name="playerIndex">
        /// which controller
        /// </param>
        /// <param name="action">
        /// name of the action
        /// </param>
        /// <returns>
        /// true if the button has been pressed
        /// false if the button not has been pressed
        /// </returns>
        public bool ButtonPressed(PlayerIndex playerIndex, string action)
        {
            if (currentGamePadState[(int)playerIndex].IsConnected)
            {
                if (gamePadActions.ContainsKey(playerIndex))
                {
                    if (gamePadActions[playerIndex].ContainsKey(action))
                    {
                        Buttons button = gamePadActions[playerIndex][action];
                        if (currentGamePadState[(int)playerIndex].IsButtonDown(button) && !previousGamePadState[(int)playerIndex].IsButtonDown(button))
                        {
                            return true;
                        }
                        
                    }
                }
            }
            else
            {

            }
            return false;
        }
    }
}
