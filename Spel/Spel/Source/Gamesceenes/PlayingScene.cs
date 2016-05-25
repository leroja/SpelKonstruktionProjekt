using GameEngine.Source.Managers;
using GameEngine.Source.Systems.Interfaces;
using GameEngine.Source.Components;
using GameEngine.Source.Enumerator;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Spel.Source.Enum;
using Spel.Source.Systems;
using Spel.Source.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spel.Source.Gamestates
{
    class PlayingScene : IGamescene
    {
        public List<int> entitiesInState { get; set; }
        private List<int> recycle;

        /// <summary>
        /// PlayingScene constructor, is responsible for containg the enteties which is in the playing state of the gameplay.
        /// </summary>
        public PlayingScene()
        {
            entitiesInState = new List<int>();
            recycle = new List<int>();
            ChangeCubesSystem ccs = (ChangeCubesSystem)SystemManager.Instance.RetrieveSystem<IUpdate>("ChangeCubesSystem");
            ccs.Initialize();
            SpawnPowerUpSystem sps = (SpawnPowerUpSystem)SystemManager.Instance.RetrieveSystem<IUpdate>("SpawnPowerUpSystem");
            sps.Initialize();
            List<int> maps = ComponentManager.Instance.GetAllEntitiesWithComponentType<DrawableTextComponent>();
            if (maps.Count == 1)
            {
                int temp = maps.First();
                DrawableTextComponent draw = ComponentManager.Instance.GetEntityComponent<DrawableTextComponent>(temp);
                switch (draw.text)
                {
                    case "Whiteboard":
                        WhiteboardMap();
                        break;
                    case "Temp":
                        tempMap();
                        break;
                    case "Random":
                        randomMap();
                        break;      
                }
            }
            List<int> Players = ComponentManager.Instance.GetAllEntitiesWithComponentType<PlayerComponent>();
            int i = 1;
            foreach (var play in Players)
            {
                DrawableComponent tempDraw = ComponentManager.Instance.GetEntityComponent<DrawableComponent>(play);
                KeyBoardComponent tempkey = ComponentManager.Instance.GetEntityComponent<KeyBoardComponent>(play);
                PositionComponent temppos = ComponentManager.Instance.GetEntityComponent<PositionComponent>(play);
                Keys key;
                tempkey.keyBoardActions.TryGetValue(ActionsEnum.Up, out key);
                entitiesInState.Add(GameEntityFactory.Instance.CreatePlayer(true, false, Buttons.A, key, temppos.position, "Player " + i, Direction.Right, PlayerIndex.One, tempDraw.colour));
                i++;
                recycle.Add(play);
            }
            GameEntityFactory.Instance.CreateAIPlayer(Direction.Right, new Vector2(200, 500), true, "AI one", Color.Red);
            SceneSystem.Instance.clearScene(recycle);
            

           
            //The enteties which is special for the playing state of the gameplay could be created and added here.
            //GameEntityFactory.Instance.CreatePlayer(true, false, Buttons.A, Keys.Up, new Vector2(Game.Instance.GraphicsDevice.Viewport.Width / 2, 10), "Kanin 1", Direction.Left, PlayerIndex.One, Color.Green);
            //GameEntityFactory.Instance.CreatePlayer(true, false, Buttons.B, Keys.W, Vector2.One, "Kanin 2", Direction.Right, PlayerIndex.Two, Color.White);
           


        }

        private void randomMap()
        {

            Random rand = new Random();
            int sound = rand.Next(0, 2);
            switch (sound)
            {
                case 0:
                    AudioManager.Instance.PlaySong("ljus");
                    AudioManager.Instance.ChangeRepeat(true);
                    AudioManager.Instance.ChangeSongVolume(0.4f);
                    break;
                case 1:
                    AudioManager.Instance.PlaySong("metal");
                    AudioManager.Instance.ChangeRepeat(true);
                    AudioManager.Instance.ChangeSongVolume(0.4f);
                    break;
                case 2:
                    AudioManager.Instance.PlaySong("sax");
                    AudioManager.Instance.ChangeRepeat(true);
                    AudioManager.Instance.ChangeSongVolume(0.4f);
                    break;
            }
            GameEntityFactory.Instance.CreateBorderRecs(Vector2.Zero, Game.Instance.GraphicsDevice.Viewport.Width, 0, Wall.TopWall);
            GameEntityFactory.Instance.CreateBorderRecs(Vector2.Zero, 0, Game.Instance.GraphicsDevice.Viewport.Height, Wall.LeftWall);
            GameEntityFactory.Instance.CreateBorderRecs(new Vector2(0, Game.Instance.GraphicsDevice.Viewport.Height), Game.Instance.GraphicsDevice.Viewport.Width, 0, Wall.BottomWall);
            GameEntityFactory.Instance.CreateBorderRecs(new Vector2(Game.Instance.GraphicsDevice.Viewport.Width, 0), 0, Game.Instance.GraphicsDevice.Viewport.Height, Wall.RightWall);
            float temp = rand.Next(0,Game.Instance.GraphicsDevice.Viewport.Width) * 0.75f;
            float temp2 = rand.Next(0,Game.Instance.GraphicsDevice.Viewport.Width) * 0.75f;
            float temp3 = rand.Next(0,Game.Instance.GraphicsDevice.Viewport.Height) * 0.75f;
            float temp4 = rand.Next(0,Game.Instance.GraphicsDevice.Viewport.Height) * 0.75f;
            GameEntityFactory.Instance.CreatePlatform(new Vector2(temp, temp3), "suddis", 150, 20);
            GameEntityFactory.Instance.CreatePlatform(new Vector2(temp2, temp4), "suddis", 150, 20);

        }

        private void tempMap()
        {
            AudioManager.Instance.PlaySong("ljus");
            AudioManager.Instance.ChangeRepeat(true);
            AudioManager.Instance.ChangeSongVolume(0.4f);
            GameEntityFactory.Instance.CreateBorderRecs(Vector2.Zero, Game.Instance.GraphicsDevice.Viewport.Width, 0, Wall.TopWall);
            GameEntityFactory.Instance.CreateBorderRecs(Vector2.Zero, 0, Game.Instance.GraphicsDevice.Viewport.Height, Wall.LeftWall);
            GameEntityFactory.Instance.CreateBorderRecs(new Vector2(0, Game.Instance.GraphicsDevice.Viewport.Height), Game.Instance.GraphicsDevice.Viewport.Width, 0, Wall.BottomWall);
            GameEntityFactory.Instance.CreateBorderRecs(new Vector2(Game.Instance.GraphicsDevice.Viewport.Width, 0), 0, Game.Instance.GraphicsDevice.Viewport.Height, Wall.RightWall);
            GameEntityFactory.Instance.CreatePlatform(new Vector2(800, 250), "suddis", 150, 20);
            GameEntityFactory.Instance.CreatePlatform(new Vector2(200, 500), "suddis", 150, 20);
        }

        private void WhiteboardMap()
        {
            AudioManager.Instance.PlaySong("metal");
            AudioManager.Instance.ChangeRepeat(true);
            AudioManager.Instance.ChangeSongVolume(0.4f);
            GameEntityFactory.Instance.CreateBorderRecs(Vector2.Zero, Game.Instance.GraphicsDevice.Viewport.Width, 0, Wall.TopWall);
            GameEntityFactory.Instance.CreateBorderRecs(Vector2.Zero, 0, Game.Instance.GraphicsDevice.Viewport.Height, Wall.LeftWall);
            GameEntityFactory.Instance.CreateBorderRecs(new Vector2(0, Game.Instance.GraphicsDevice.Viewport.Height), Game.Instance.GraphicsDevice.Viewport.Width, 0, Wall.BottomWall);
            GameEntityFactory.Instance.CreateBorderRecs(new Vector2(Game.Instance.GraphicsDevice.Viewport.Width, 0), 0, Game.Instance.GraphicsDevice.Viewport.Height, Wall.RightWall);
            GameEntityFactory.Instance.CreatePlatform(new Vector2(200, 250), "suddis", 150, 20);
            GameEntityFactory.Instance.CreatePlatform(new Vector2(800, 500), "suddis", 150, 20);
        }

        //Not sure if the functios blow is needed, because of the more general methods in the scenemanager.
        /// <summary>
        /// addEntity function is used for adding a entity to a the gameplay which cannot be added by the PlayingState class. For example the players
        /// cause we need the information from the start-up sceene, there fore the players should be added by calling this function before leaving that state.
        /// </summary>
        /// <param name="index">an int which represent the index of the entity of the player which needs to be added</param>
        public void addEntity(int index)
        {
            entitiesInState.Add(index);
        }
        /// <summary>
        /// removeEntity function is used for removing a entity durring the playing part of the game, for example when a player dies the entity for the player should
        /// be removed from the gameplay.
        /// </summary>
        /// <param name="index"></param>
        public void removeEntity(int index)
        {
            entitiesInState.Remove(index);
        }
        /// <summary>
        /// initializeMap function is used to create the entities for the layout of the map which should be used durring the gameplay.
        /// This should be called before leaving the setup gamestate.
        /// </summary>
        /// <param name="map">is a Texture2D object which represents the layout of the map</param>
        public void initializeMap(Texture2D map)
        {
            //This should create the enteties which is needed durring the gameplay. Read all pictures of the map and depending on colour create enteties and so forth.
        }

        /// <summary>
        /// onSceneCreated this function is called whenever the current gamestate is changed. This function should contain logic that 
        /// needs to be processed before the state is shown for the player. This could be enteties that's not able to be created pre-runtime.
        /// </summary>
        public void onSceneCreated()
        {

        }

        /// <summary>
        /// onSceneUpdate this function handles the logic for the state which should be run durring the update partion of the game.
        /// For example this could be to check for conditions to continue to the next state of the gameplay. 
        /// </summary>
        public void onSceneUpdate()
        {

        }
    }
}
