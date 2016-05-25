using GameEngine.Source.Managers;
using GameEngine.Source.Systems.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Spel.Source.Enum;
using Spel.Source.Systems;
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

        /// <summary>
        /// PlayingScene constructor, is responsible for containg the enteties which is in the playing state of the gameplay.
        /// </summary>
        public PlayingScene()
        {
            entitiesInState = new List<int>();
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
            
            AudioManager.Instance.PlaySong("metal");
            AudioManager.Instance.ChangeRepeat(true);
            AudioManager.Instance.ChangeSongVolume(0.3f);
            ChangeCubesSystem ccs = (ChangeCubesSystem)SystemManager.Instance.RetrieveSystem<IUpdate>("ChangeCubesSystem");
            ccs.Initialize();
            SpawnPowerUpSystem sps = (SpawnPowerUpSystem)SystemManager.Instance.RetrieveSystem<IUpdate>("SpawnPowerUpSystem");
            sps.Initialize();
            //The enteties which is special for the playing state of the gameplay could be created and added here.
            entitiesInState.Add(GameEntityFactory.Instance.CreatePlayer(true, false, Buttons.A, Keys.Up, new Vector2(Game.Instance.GraphicsDevice.Viewport.Width / 2, 10), "Kanin 1", Direction.Left, PlayerIndex.One, Color.Green));
            entitiesInState.Add(GameEntityFactory.Instance.CreatePlayer(true, false, Buttons.B, Keys.W, Vector2.One, "Kanin 2", Direction.Right, PlayerIndex.Two, Color.White));
            entitiesInState.Add(GameEntityFactory.Instance.CreateBorderRecs(Vector2.Zero, Game.Instance.GraphicsDevice.Viewport.Width, 0, Wall.TopWall));
            entitiesInState.Add(GameEntityFactory.Instance.CreateBorderRecs(Vector2.Zero, 0, Game.Instance.GraphicsDevice.Viewport.Height, Wall.LeftWall));
            entitiesInState.Add(GameEntityFactory.Instance.CreateBorderRecs(new Vector2(0, Game.Instance.GraphicsDevice.Viewport.Height), Game.Instance.GraphicsDevice.Viewport.Width, 0, Wall.BottomWall));
            entitiesInState.Add(GameEntityFactory.Instance.CreateBorderRecs(new Vector2(Game.Instance.GraphicsDevice.Viewport.Width, 0), 0, Game.Instance.GraphicsDevice.Viewport.Height, Wall.RightWall));
            entitiesInState.Add(GameEntityFactory.Instance.CreateAIPlayer(Direction.Right, new Vector2(200, 500), true, "AI one", Color.White));
            entitiesInState.Add(GameEntityFactory.Instance.CreatePlatform(new Vector2(200, 250), "suddis", 150, 20));
            entitiesInState.Add(GameEntityFactory.Instance.CreatePlatform(new Vector2(800, 500), "suddis", 150, 20));
        }

        /// <summary>
        /// onSceneUpdate this function handles the logic for the state which should be run durring the update partion of the game.
        /// For example this could be to check for conditions to continue to the next state of the gameplay. 
        /// </summary>
        public void onSceneUpdate()
        {
            HealthSystem hs = (HealthSystem)SystemManager.Instance.RetrieveSystem<IUpdate>("HealthSystem");
            

            if (hs != null)
            {
                List<int> dt = hs.getLivingPlayers();
                if (dt != null && dt.Count == 1)
                {
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
