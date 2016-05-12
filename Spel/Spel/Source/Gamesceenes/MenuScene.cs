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

namespace Spel.Menus
{
    /// <summary>
    /// MenuScene class is responsible for handling entities which should be displayed for the menu scene's
    /// </summary>
    public class MenuScene : IGamescene
    {
        public List<int> entitiesInState
        {
            get; set;
        }
        private float x;
        private float y;
        private string [] menuItems;
        private DrawableTextComponent textcomp;
        /// <summary>
        /// MenuScene constructor is responsible for creating the entities for the menu. The default position
        /// for the entities in the menu will be somewhere in the middle of the screen.
        /// </summary>
        /// <param name="menuItems">Takes a String array which contains the strings which should be displayed as the menu.</param>
        public MenuScene(string[] menuItems)
        {
            entitiesInState = new List<int>();

            this.x = GraphicsDeviceManager.DefaultBackBufferHeight / 2;
            this.y = GraphicsDeviceManager.DefaultBackBufferWidth / 2;
            this.menuItems = menuItems;
            
        }

        /// <summary>
        /// MenuScene constructor, if the placement of the menu is desired somewhere else on the screen then this constructor could be used.
        /// </summary>
        /// <param name="menuItems">Is an array of strings which should be displayed as the menu</param>
        /// <param name="startingPosX">is a float which is the desired x coordinate for the starting point of the menu</param>
        /// <param name="startingPosY">is a float which is the desired y coordinate for the starting point of the menu</param>
        public MenuScene(string[] menuItems, float startingPosX, float startingPosY)
        {
            entitiesInState = new List<int>();
            this.menuItems = menuItems;
            this.x = startingPosX;
            this.y = startingPosY;
        }


        /// <summary>
        /// onSceneCreated this function is called whenever the current gamestate is changed. This function should contain logic that 
        /// needs to be processed before the state is shown for the player. This could be enteties that's not able to be created pre-runtime.
        /// </summary>
        public void onSceneCreated()
        {
            Dictionary<Vector2, String> menuList = new Dictionary<Vector2, string>();
            
            int i = 0;
            float yvar = this.y;
            foreach (string a in menuItems)
            {
                menuList.Add(new Vector2(this.x, yvar), menuItems[i]);
                yvar += 30;
                i++;
            }
            textcomp = new DrawableTextComponent(menuList, Game.Instance.GetContent<SpriteFont>("Fonts/MenuFont"), Color.Black);
            int id = ComponentManager.Instance.CreateID();
            ComponentManager.Instance.AddComponentToEntity(id, textcomp);
            entitiesInState.Add(id);

            Texture2D arrowPix = Game.Instance.GetContent<Texture2D>("pic/arrow");
            DrawableComponent arrow = new DrawableComponent(arrowPix);
            PositionComponent arrowPos = new PositionComponent(new Vector2(this.x - 35, this.y));
            int arrowId = ComponentManager.Instance.CreateID();
            ComponentManager.Instance.AddComponentToEntity(arrowId, arrow);
            ComponentManager.Instance.AddComponentToEntity(arrowId, arrowPos);
            KeyBoardComponent kbcArrow = new KeyBoardComponent();
            

            ComponentManager.Instance.AddComponentToEntity(arrowId, kbcArrow);

        
            entitiesInState.Add(id);
        }

        /// <summary>
        /// onSceneUpdate this function handles the logic for the state which should be run durring the update partion of the game.
        /// For example this could be to check for conditions to continue to the next state of the gameplay. 
        /// </summary>
        public void onSceneUpdate()
        {
            Game game = Game.Instance;
            if (textcomp.selectedIndex == 0 && textcomp.controlKeys(Keys.Enter))
                game.state = new PlayingScene();
            else if (textcomp.selectedIndex == 1 && textcomp.controlKeys(Keys.Enter))
                game.state = new OptionsScene();
            else if (textcomp.selectedIndex == 2 && textcomp.controlKeys(Keys.Enter))
                game.Exit();
            return;
        }
    }
}
