using GameEngine.Source.Components;
using GameEngine.Source.Managers;
using GameEngine.Source.Systems.Interfaces;
using GameEngine.Source.Enumerator;
using Spel.Source.Components;
using Spel.Source.Systems;
using Spel.Source.Gamesceenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Spel.Menus;

namespace Spel.Source.Gamestates
{
    /// <summary>
    /// EndingSceene is the class responsible for the ending state of the gameplay.
    /// </summary>
    class EndingScene : IGamescene
    {
        public List<int> entitiesInState { get; set; }
        private KeyBoardComponent kbc;

        /// <summary>
        /// endingScene constructor is responsible for managing the enteties which is used durring the 
        /// ending state of the gameplay, when someone is pronunced the winner
        /// </summary>
        public EndingScene()
        {
            /// some background, text and some presentation of the winner of the game.
            entitiesInState = new List<int>();
        }
        /// <summary>
        /// onSceneCreated this function is called whenever the current gamestate is changed. This function should contain logic that 
        /// needs to be processed before the state is shown for the player. This could be enteties that's not able to be created pre-runtime.
        /// </summary>
        public void onSceneCreated()
        {
            SpriteFont font1 = Game.Instance.GetContent<SpriteFont>("Fonts/Menufont");
            DrawableTextComponent winner = new DrawableTextComponent("Winner:", Color.Black, font1);
            PositionComponent pos = new PositionComponent(new Vector2(Game.Instance.GraphicsDevice.Viewport.Width * 0.5f - ((float)font1.MeasureString("Winner:").X * 0.5f), 0));
            kbc = new KeyBoardComponent();
            int WinnerId = ComponentManager.Instance.CreateID();
            kbc.keyBoardActions.Add(ActionsEnum.Up, Keys.Enter);
            ComponentManager.Instance.AddComponentToEntity(WinnerId, winner);
            ComponentManager.Instance.AddComponentToEntity(WinnerId, kbc);
            ComponentManager.Instance.AddComponentToEntity(WinnerId, pos);
            entitiesInState.Add(WinnerId);

            HealthSystem hs = (HealthSystem)SystemManager.Instance.RetrieveSystem<IUpdate>("HealthSystem");
            int id = hs.deathList.First();
            hs.deathList.Clear();
            AIComponent ai = new AIComponent();
            ComponentManager.Instance.AddComponentToEntity(id, ai);
            PlayerComponent player = ComponentManager.Instance.GetEntityComponent<PlayerComponent>(id);
            entitiesInState.Add(id);
            SpriteFont font2 = Game.Instance.GetContent<SpriteFont>("Fonts/Menufont");
            DrawableTextComponent winningPlayer = new DrawableTextComponent(player.playerName, Color.Black, font2);
            PositionComponent pos2 = new PositionComponent(new Vector2(Game.Instance.GraphicsDevice.Viewport.Width * 0.5f - ((float)font2.MeasureString(player.playerName).X * 0.5f), 50));
            int textId2 = ComponentManager.Instance.CreateID();
            ComponentManager.Instance.AddComponentToEntity(textId2, winningPlayer);
            ComponentManager.Instance.AddComponentToEntity(textId2, pos2);
            entitiesInState.Add(textId2);

            DrawableTextComponent text = new DrawableTextComponent("Press enter to return to Main menu", Color.Black, Game.Instance.GetContent<SpriteFont>("Fonts/Menufont"));
            PositionComponent poss = new PositionComponent(new Vector2(0, Game.Instance.GraphicsDevice.Viewport.Height - 100));
            int textId = ComponentManager.Instance.CreateID();
            ComponentManager.Instance.AddComponentToEntity(textId, text);
            ComponentManager.Instance.AddComponentToEntity(textId, poss);
            entitiesInState.Add(textId);

            int se = ComponentManager.Instance.CreateID();
            ComponentManager.Instance.AddComponentToEntity(se, new SoundEffectComponent("winner"));
            entitiesInState.Add(se);
        }
        /// <summary>
        /// onSceneUpdate this function handles the logic for the state which should be run durring the update partion of the game.
        /// For example this could be to check for conditions to continue to the next state of the gameplay. 
        /// </summary>
        public void onSceneUpdate()
        {
            if (kbc.state[ActionsEnum.Up] == ButtonStates.Pressed)
            {
                List<int> tempura = ComponentManager.Instance.GetAllEntitiesWithComponentType<AIComponent>();
                SceneSystem.Instance.clearScene(tempura);
                List<int> players = ComponentManager.Instance.GetAllEntitiesWithComponentType<PlayerComponent>();
                string[] menuItems = { "Start Game", "Options", "About", "End Game" };
                SceneSystem.Instance.clearScene(entitiesInState);
                ComponentManager.Instance.Clear();
                HealthSystem hs = (HealthSystem)SystemManager.Instance.RetrieveSystem<IUpdate>("HealthSystem");
                hs.initialize();
                SceneSystem.Instance.setCurrentScene(new MenuScene(menuItems));
            }
        }
    }
}
