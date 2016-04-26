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
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <param name="key"></param>
        public void AddKeyBoardAction(string action, Keys key)
        {
            if (keyBoardActions.ContainsKey(action))
            {
                keyBoardActions.Remove(action);
            }
            keyBoardActions.Add(action, key);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        public void RemoveKeyBoardAction(string action)
        {
            if (keyBoardActions.ContainsKey(action))
            {
                keyBoardActions.Remove(action);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <param name="playerIndex"></param>
        /// <param name="action"></param>
        /// <param name="button"></param>
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
        /// 
        /// </summary>
        /// <param name="playerIndex"></param>
        /// <param name="action"></param>
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
        /// 
        /// </summary>
        /// <param name="playerIndex"></param>
        /// <param name="action"></param>
        /// <returns></returns>
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
