using GameEngine.Source.Systems.Interfaces;
using Microsoft.Xna.Framework.Graphics;
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
            //The enteties which is special for the playing state of the gameplay could be created and added here.

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
