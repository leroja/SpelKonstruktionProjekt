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
using Spel.Source.Enum;
using Spel.Source.Components;
namespace Spel.Source.Gamestates
{
    /// <summary>
    /// SetUpPlayerScene is the state responsible for the startup state where the players are created.
    /// </summary>
    class SetUpPlayerScene : IGamescene
    {
        public List<int> entitiesInState { get; set; }
        private List<int> Players;
        private List<Keys> UnAvailableKeys;
        private int textId;
        private int Index;
        private int newId;
        private Map map;
        private KeyBoardComponent kbcArrow;
        private KeyBoardComponent kbc1;
        private DrawableTextComponent draw;


        /// <summary>
        /// SetUpPlayerState constructor, which is responsible for adding enteties to the scene in the gameplay where the players choose their caracters and
        /// controlls etc.
        /// </summary>
        public SetUpPlayerScene()
        {
            entitiesInState = new List<int>();
            Players = new List<int>();
            UnAvailableKeys = new List<Keys>();
            UnAvailableKeys.Add(Keys.Enter);
            UnAvailableKeys.Add(Keys.Right);
        }

        /// <summary>
        /// onSceneCreated this function is called whenever the current gamestate is changed. This function should contain logic that 
        /// needs to be processed before the state is shown for the player. This could be enteties that's not able to be created pre-runtime.
        /// </summary>
        public void onSceneCreated()
        {

            DrawableTextComponent text1 = new DrawableTextComponent("Map:", Color.Black, Game.Instance.GetContent<SpriteFont>("Fonts/Menufont"));
            PositionComponent pos1 = new PositionComponent(new Vector2(0, Game.Instance.GraphicsDevice.Viewport.Height - 80));
            int id1 = ComponentManager.Instance.CreateID();
            ComponentManager.Instance.AddComponentToEntity(id1, text1);
            ComponentManager.Instance.AddComponentToEntity(id1, pos1);
            entitiesInState.Add(id1);

            draw = new DrawableTextComponent("Whiteboard", Color.Black, Game.Instance.GetContent<SpriteFont>("Fonts/Menufont"));
            PositionComponent pos2 = new PositionComponent(new Vector2(105, Game.Instance.GraphicsDevice.Viewport.Height - 80));
            kbc1 = new KeyBoardComponent();
            kbc1.keyBoardActions.Add(ActionsEnum.Left, Keys.Right);
            newId = ComponentManager.Instance.CreateID();
            ComponentManager.Instance.AddComponentToEntity(newId, draw);
            ComponentManager.Instance.AddComponentToEntity(newId, pos2);
            ComponentManager.Instance.AddComponentToEntity(newId, kbc1);


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

            if (kbc1.state[ActionsEnum.Left] == ButtonStates.Pressed)
            {
                map = map + 1;
                if (map > Map.Random)
                    map = Map.Whiteboard;

                switch (map)
                {
                    case Map.Whiteboard:
                        draw.text = "Whiteboard";
                        break;
                    case Map.Temp:

                        draw.text = "Temp";
                        break;
                    case Map.Random:

                        draw.text = "Random";
                        break;
                }
            }
            if (temp.state[ActionsEnum.Enter] == ButtonStates.Pressed)
            {
                DrawableTextComponent temp54 = ComponentManager.Instance.GetEntityComponent<DrawableTextComponent>(newId);
                temp54.visable = false;
                SceneSystem.Instance.clearScene(entitiesInState);
                SceneSystem.Instance.setCurrentScene(new PlayingScene());
            }
            

            KeyboardState tetet = Keyboard.GetState();
            List<Keys> key = tetet.GetPressedKeys().ToList<Keys>();

            if (key.Count != 0)
            {


                if (UnAvailableKeys != null && !UnAvailableKeys.Contains(key[0]))
                {
                    Keys temo = key[0];
                    key.Clear();
                    Dictionary<int, Keys> Dir = new Dictionary<int, Keys>();
                    int tempId = ComponentManager.Instance.CreateID();
                    int count = Players.Count;
                    Players.Add(tempId);
                    DrawableComponent tempDraw = new DrawableComponent(Game.Instance.GetContent<Texture2D>("Pic/kanin1"), SpriteEffects.None);
                    PositionComponent temppos = new PositionComponent(new Vector2((Game.Instance.GraphicsDevice.Viewport.Width * 0.25f+ 20f) * count, Game.Instance.GraphicsDevice.Viewport.Height * 0.5f));
                    KeyBoardComponent tempkey = new KeyBoardComponent();
                    PlayerComponent tempplay = new PlayerComponent();
                    tempkey.keyBoardActions.Add(ActionsEnum.Up, temo);
                    ComponentManager.Instance.AddComponentToEntity(tempId, tempDraw);
                    ComponentManager.Instance.AddComponentToEntity(tempId, temppos);
                    ComponentManager.Instance.AddComponentToEntity(tempId, tempkey);
                    ComponentManager.Instance.AddComponentToEntity(tempId, tempplay);
                    
                
                    //Keys temp0;
                    //tempkey.keyBoardActions.TryGetValue(ActionsEnum.Up, out temp0);
                    //Console.WriteLine(temp0.ToString());
                    

                    int textId = ComponentManager.Instance.CreateID();
                    DrawableTextComponent tempDrawtext = new DrawableTextComponent(temo.ToString(), Color.Black, Game.Instance.GetContent<SpriteFont>("Fonts/MenuFont"));
                    PositionComponent temptextPos = new PositionComponent(new Vector2((Game.Instance.GraphicsDevice.Viewport.Width * 0.25f+20f) * count, Game.Instance.GraphicsDevice.Viewport.Height * 0.5f + 80f));
                    ComponentManager.Instance.AddComponentToEntity(textId, tempDrawtext);
                    ComponentManager.Instance.AddComponentToEntity(textId, temptextPos);
                    entitiesInState.Add(textId);
                    UnAvailableKeys.Add(temo);
                }
                else if (UnAvailableKeys.Contains(key[0]) && key[0] != Keys.Enter)
                {
                    foreach (var a in Players)
                    {
                        KeyBoardComponent tempa = ComponentManager.Instance.GetEntityComponent<KeyBoardComponent>(a);
                        Keys key2;
                        tempa.keyBoardActions.TryGetValue(ActionsEnum.Up, out key2);
                        if (tempa != null && key2 != Keys.Enter)
                        {
                            if (tempa.state[ActionsEnum.Up] == ButtonStates.Pressed)
                            {
                                DrawableComponent tempura = ComponentManager.Instance.GetEntityComponent<DrawableComponent>(a);
                                Random rand = new Random();
                                tempura.colour = new Color(rand.Next(0, 256), rand.Next(0, 256), rand.Next(0, 256));

                            }
                        }
                    }
                }

            }
        }
    }
}
