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
using Spel.Source.Systems;

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
        private int index;
        private float x;
        private float y;
        private string [] menuItems;
        private DrawableTextComponent textcomp;
        private KeyBoardComponent kbcArrow;
        private int arrowId;


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
            ScrollingBackgroundSystem temp =(ScrollingBackgroundSystem)SystemManager.Instance.RetrieveSystem<IDraw>("ScrollingBackgroundSystem");
            temp.active = true;
            Dictionary<Vector2, String> menuList = new Dictionary<Vector2, string>();
            
            int i = 0;
            float yvar = this.y;
            foreach (string a in menuItems)
            {
                menuList.Add(new Vector2(this.x, yvar-200), menuItems[i]);
                yvar += 50;
                i++;
            }
            textcomp = new DrawableTextComponent(menuList, Game.Instance.GetContent<SpriteFont>("Fonts/MenuFont"), Color.Black);
            index = 0;
            int Id = ComponentManager.Instance.CreateID();
            ComponentManager.Instance.AddComponentToEntity(Id, textcomp);
            entitiesInState.Add(Id);

            Texture2D arrowPix = Game.Instance.GetContent<Texture2D>("pic/arrow");
            DrawableComponent arrow = new DrawableComponent(arrowPix, SpriteEffects.None);
            PositionComponent arrowPos = new PositionComponent(new Vector2(this.x - 35, this.y-200));
            MovementComponent arrowMovement = new MovementComponent(new Vector2(x, y));
            arrowId = ComponentManager.Instance.CreateID();
            ComponentManager.Instance.AddComponentToEntity(arrowId, arrow);
            ComponentManager.Instance.AddComponentToEntity(arrowId, arrowPos);
            ComponentManager.Instance.AddComponentToEntity(arrowId, arrowMovement);
            kbcArrow = new KeyBoardComponent();
            kbcArrow.keyBoardActions.Add(ActionsEnum.Down, Keys.Down);
            kbcArrow.keyBoardActions.Add(ActionsEnum.Up, Keys.Up);
            kbcArrow.keyBoardActions.Add(ActionsEnum.Enter, Keys.Enter);


            ComponentManager.Instance.AddComponentToEntity(arrowId, kbcArrow);
            
        
            entitiesInState.Add(arrowId);
        }

        /// <summary>
        /// onSceneUpdate this function handles the logic for the state which should be run durring the update partion of the game.
        /// For example this could be to check for conditions to continue to the next state of the gameplay. 
        /// </summary>
        public void onSceneUpdate()
        {

            Game game = Game.Instance;
            PositionComponent temp = ComponentManager.Instance.GetEntityComponent<PositionComponent>(arrowId);
            temp.position = temp.prevPosition;

            if (kbcArrow.state[ActionsEnum.Down] == ButtonStates.Pressed)
            {
                if (index <= 2)
                {
                    index = index + 1;
                    temp.prevPosition = temp.position;
                    temp.position.Y += 50;
                  
                    return;
                }
            }
            else if (kbcArrow.state[ActionsEnum.Up] == ButtonStates.Pressed)
            {
                if (index > 0)
                {
                    index = index - 1;
                    temp.prevPosition = temp.position;
                    temp.position.Y -= 50;
                    return;

                }
            }

            if (index == 0 && kbcArrow.state[ActionsEnum.Enter] == ButtonStates.Pressed)
            {
                ScrollingBackgroundSystem back = (ScrollingBackgroundSystem)SystemManager.Instance.RetrieveSystem<IDraw>("ScrollingBackgroundSystem");
                back.active = false;
                SceneSystem.Instance.clearScene(entitiesInState);
                SceneSystem.Instance.setCurrentScene(new SetUpPlayerScene());
            }
            if (index == 1 && kbcArrow.state[ActionsEnum.Enter] == ButtonStates.Pressed)
            {
                //game.state = new OptionsScene();
            }
            if (index == 2 && kbcArrow.state[ActionsEnum.Enter] == ButtonStates.Pressed)
            {

            }
            if (index == 3 && kbcArrow.state[ActionsEnum.Enter] == ButtonStates.Pressed)
            {
                game.Exit();
            }
        }
    }
}
