using GameEngine.Source.Components;
using GameEngine.Source.Managers;
using GameEngine.Source.Systems.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Spel.Source.Gamestates
{
    class SetUpPlayerState : IGamestate
    {
        public List<int> entetiesInState { get; set; }
        
        /// <summary>
        /// SetUpPlayerState constructor, which is responsible for adding enteties to the scene in the gameplay where the players choose their caracters and
        /// controlls etc.
        /// </summary>
        public SetUpPlayerState()
        {
            entetiesInState = new List<int>();
        }

        public void onSceneCreated()
        {
            GameEntityFactory.Instance.CreateTestKanin(true, Keys.Up, Keys.Left, Keys.Down, Keys.Right, Vector2.One, "Kanin 1");
        }
        public void onSceneUpdate()
        {

        }
    }
}
