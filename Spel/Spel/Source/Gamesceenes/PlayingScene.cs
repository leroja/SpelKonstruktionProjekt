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
        }

        private void randomMap()
        {

            Random rand = new Random();
            int sound = rand.Next(0, 3);
            sound = 2;
            AudioManager.Instance.ChangeRepeat(true);
            AudioManager.Instance.ChangeSongVolume(0.25f);
            switch (sound)
            {
                case 0:
                    AudioManager.Instance.PlaySong("ljus");
                    break;
                case 1:
                    AudioManager.Instance.PlaySong("metal");
                    break;
                case 2:
                    AudioManager.Instance.PlaySong("sax");
                    break;
            }
            GameEntityFactory.Instance.CreateBorderRecs(Vector2.Zero, Game.Instance.GraphicsDevice.Viewport.Width, 0, Wall.TopWall);
            GameEntityFactory.Instance.CreateBorderRecs(Vector2.Zero, 0, Game.Instance.GraphicsDevice.Viewport.Height, Wall.LeftWall);
            GameEntityFactory.Instance.CreateBorderRecs(new Vector2(0, Game.Instance.GraphicsDevice.Viewport.Height), Game.Instance.GraphicsDevice.Viewport.Width, 0, Wall.BottomWall);
            GameEntityFactory.Instance.CreateBorderRecs(new Vector2(Game.Instance.GraphicsDevice.Viewport.Width, 0), 0, Game.Instance.GraphicsDevice.Viewport.Height, Wall.RightWall);
            float temp = rand.Next(0, Game.Instance.GraphicsDevice.Viewport.Width) * 0.75f;
            float temp2 = rand.Next(0, Game.Instance.GraphicsDevice.Viewport.Width) * 0.75f;
            float temp3 = rand.Next(0, Game.Instance.GraphicsDevice.Viewport.Height) * 0.75f;
            float temp4 = rand.Next(0, Game.Instance.GraphicsDevice.Viewport.Height) * 0.75f;
            entitiesInState.Add(GameEntityFactory.Instance.CreatePlatform(new Vector2(temp, temp3), "suddis", 150, 20));
            entitiesInState.Add(GameEntityFactory.Instance.CreatePlatform(new Vector2(temp2, temp4), "suddis", 150, 20));

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
            entitiesInState.Add(GameEntityFactory.Instance.CreatePlatform(new Vector2(800, 250), "suddis", 150, 20));
            entitiesInState.Add(GameEntityFactory.Instance.CreatePlatform(new Vector2(200, 500), "suddis", 150, 20));
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
            entitiesInState.Add(GameEntityFactory.Instance.CreatePlatform(new Vector2(200, 250), "suddis", 150, 20));
            entitiesInState.Add(GameEntityFactory.Instance.CreatePlatform(new Vector2(800, 500), "suddis", 150, 20));
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
            ChangeCubesSystem ccs = (ChangeCubesSystem)SystemManager.Instance.RetrieveSystem<IUpdate>("ChangeCubesSystem");
            ccs.Respawn(3);
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
            SceneSystem.Instance.clearScene(maps);

            List<int> Players = ComponentManager.Instance.GetAllEntitiesWithComponentType<PlayerComponent>();
            int i = 1;

            Random rand = new Random();

            foreach (var play in Players)
            {
                DrawableComponent tempDraw = ComponentManager.Instance.GetEntityComponent<DrawableComponent>(play);
                KeyBoardComponent tempkey = ComponentManager.Instance.GetEntityComponent<KeyBoardComponent>(play);
                PositionComponent temppos = ComponentManager.Instance.GetEntityComponent<PositionComponent>(play);
                Keys key;
                tempkey.keyBoardActions.TryGetValue(ActionsEnum.Up, out key);
                entitiesInState.Add(GameEntityFactory.Instance.CreatePlayer(true, false, Buttons.A, key, new Vector2(rand.Next((int)(Game.Instance.GraphicsDevice.Viewport.Width * 0.8)), (int)(Game.Instance.GraphicsDevice.Viewport.Height * 0.8)), "Player " + i, Direction.Right, PlayerIndex.One, tempDraw.colour));
                i++;
                ComponentManager.Instance.RemoveEntity(play);
                //ComponentManager.Instance.RecycleID(play);
            }
            entitiesInState.Add(GameEntityFactory.Instance.CreateAIPlayer(Direction.Right, new Vector2(rand.Next((int)(Game.Instance.GraphicsDevice.Viewport.Width * 0.8)), (int)(Game.Instance.GraphicsDevice.Viewport.Height * 0.8)), true, "AI one", Color.Red));


        }

        /// <summary>
        /// onSceneUpdate this function handles the logic for the state which should be run durring the update partion of the game.
        /// For example this could be to check for conditions to continue to the next state of the gameplay. 
        /// </summary>
        public void onSceneUpdate()
        {

            HealthSystem hs = (HealthSystem)SystemManager.Instance.RetrieveSystem<IUpdate>("HealthSystem");

            List<int> players = ComponentManager.Instance.GetAllEntitiesWithComponentType<PlayerComponent>();
            foreach (int p in players)
            {
                PositionComponent pc = ComponentManager.Instance.GetEntityComponent<PositionComponent>(p);

                if (ComponentManager.Instance.CheckIfEntityHasComponent<OnFloorComponent>(p) && pc.position.Y < 700)
                {
                    OnFloorComponent fc = ComponentManager.Instance.GetEntityComponent<OnFloorComponent>(p);
                    ComponentManager.Instance.RemoveComponentFromEntity(p, fc);
                }

                if (hs != null)
                {
                    List<int> dt = hs.getLivingPlayers();
                    if (dt != null && dt.Count == 1)
                    {
                        SpawnPowerUpSystem sps = (SpawnPowerUpSystem)SystemManager.Instance.RetrieveSystem<IUpdate>("SpawnPowerUpSystem");
                        sps.Initialize();
                        sps.run = false;
                        ChangeCubesSystem ccs = (ChangeCubesSystem)SystemManager.Instance.RetrieveSystem<IUpdate>("ChangeCubesSystem");
                        ccs.Respawn(0);
                        AudioManager.Instance.StopSong();
                        int id = dt.First();
                        entitiesInState.Remove(id);
                        SceneSystem.Instance.clearScene(entitiesInState);
                        SceneSystem.Instance.setCurrentScene(new EndingScene());
                    }
                }
            }
        }
    }
}