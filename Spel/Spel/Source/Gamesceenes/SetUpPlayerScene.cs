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
    /// <summary>
    /// SetUpPlayerScene is the state responsible for the startup state where the players are created.
    /// </summary>
    class SetUpPlayerScene : IGamescene
    {
        public List<int> entitiesInState { get; set; }
        private bool professor;
        
        /// <summary>
        /// SetUpPlayerState constructor, which is responsible for adding enteties to the scene in the gameplay where the players choose their caracters and
        /// controlls etc.
        /// </summary>
        public SetUpPlayerScene()
        {
            entitiesInState = new List<int>();
            professor = false;
        }

        /// <summary>
        /// onSceneCreated this function is called whenever the current gamestate is changed. This function should contain logic that 
        /// needs to be processed before the state is shown for the player. This could be enteties that's not able to be created pre-runtime.
        /// </summary>
        public void onSceneCreated()
        {
            GameEntityFactory.Instance.CreateTestKanin(true, Keys.W, Vector2.One, "Alexander");
            GameEntityFactory.Instance.CreateTestKanin(true, Keys.Up, new Vector2(300, 400), "Helmut");
        }

        /// <summary>
        /// onSceneUpdate this function handles the logic for the state which should be run durring the update partion of the game.
        /// For example this could be to check for conditions to continue to the next state of the gameplay. 
        /// </summary>
        public void onSceneUpdate()
        {
            if(professor == false)
            {

                Texture2D text = Game.Instance.GetContent<Texture2D>("Pic/professor");
                DrawableComponent comp2 = new DrawableComponent(text);
                PositionComponent pos2 = new PositionComponent(new Vector2(1, 1));
                AnimationComponent ani = new AnimationComponent(64, 64, text.Width, text.Height, 0.1);
                int id2 = ComponentManager.Instance.CreateID();
                ComponentManager.Instance.AddComponentToEntity(id2, comp2);
                ComponentManager.Instance.AddComponentToEntity(id2, pos2);
                ComponentManager.Instance.AddComponentToEntity(id2, ani);

                professor = true;
            }
        }
    }
}
