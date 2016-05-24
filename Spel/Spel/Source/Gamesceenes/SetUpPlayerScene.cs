using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine;
using Microsoft.Xna.Framework.Input;
using GameEngine.Source.Enumerator;
using Spel.Menus;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using GameEngine.Source.Systems.Interfaces;
using GameEngine.Source.Components;
using GameEngine.Source.Managers;
using Spel.Source.Gamestates;
using Spel.Source.Gamesceenes;
namespace Spel.Source.Gamestates
{
    /// <summary>
    /// SetUpPlayerScene is the state responsible for the startup state where the players are created.
    /// </summary>
    class SetUpPlayerScene : IGamescene
    {
        public List<int> entitiesInState { get; set; }
        private int textId;


        /// <summary>
        /// SetUpPlayerState constructor, which is responsible for adding enteties to the scene in the gameplay where the players choose their caracters and
        /// controlls etc.
        /// </summary>
        public SetUpPlayerScene()
        {
            entitiesInState = new List<int>();
            
        }

        /// <summary>
        /// onSceneCreated this function is called whenever the current gamestate is changed. This function should contain logic that 
        /// needs to be processed before the state is shown for the player. This could be enteties that's not able to be created pre-runtime.
        /// </summary>
        public void onSceneCreated()
        {
            DrawableTextComponent text = new DrawableTextComponent("Press Enter To Start", Color.Black, Game.Instance.GetContent<SpriteFont>("Fonts/Menufont"));
            PositionComponent pos = new PositionComponent(new Vector2(300, 0));
            KeyBoardComponent kbc = new KeyBoardComponent();
            kbc.keyBoardActions.Add(ActionsEnum.Enter, Keys.Enter);
            textId = ComponentManager.Instance.CreateID();
            ComponentManager.Instance.AddComponentToEntity(textId, kbc);
            ComponentManager.Instance.AddComponentToEntity(textId, text);
            ComponentManager.Instance.AddComponentToEntity(textId, pos);
            entitiesInState.Add(textId);
        }

        /// <summary>
        /// onSceneUpdate this function handles the logic for the state which should be run durring the update partion of the game.
        /// For example this could be to check for conditions to continue to the next state of the gameplay. 
        /// </summary>
        public void onSceneUpdate()
        {
            Game game = Game.Instance;
            KeyBoardComponent temp = ComponentManager.Instance.GetEntityComponent<KeyBoardComponent>(textId);
            if (temp.state[ActionsEnum.Enter] == ButtonStates.Pressed)
            {
                SceneSystem.Instance.clearScene(entitiesInState);
                SceneSystem.Instance.setCurrentScene(new PlayingScene());
            }
        }
    }
}
