using GameEngine.Source.Components;
using GameEngine.Source.Managers;
using GameEngine.Source.Systems.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        /// <summary>
        /// InitializeState method is used for initializing the enteties/object which is needed in the startupscreen.
        /// </summary>
        public void initializeState()
        {
            //add the enteties which should be displayed on the screen when the players choose their caracters. Player enteties is to be created after leaving this state therefore the 
            //add entetiestolist-function needs to be called before entering the playing-state

            DrawableTextComponent dtx = new DrawableTextComponent("Flappy Ass, version 1.0 By: PT2", Color.WhiteSmoke, Game.Inst().GetContent<SpriteFont>("Fonts/MenuFont"));
            PositionComponent pc = new PositionComponent(new Vector2(20, 100));

            int id = ComponentManager.Instance.CreateID();

            ComponentManager.Instance.AddComponentToEntity(id, dtx);
            ComponentManager.Instance.AddComponentToEntity(id, pc);

            entetiesInState.Add(id);
        }
    }
}
