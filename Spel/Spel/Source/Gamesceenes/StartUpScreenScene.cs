using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Source.Systems.Interfaces;
using System.Timers;
using GameEngine.Source.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameEngine.Source.Managers;
using Spel.Menus;

namespace Spel.Source.Gamestates
{
    /// <summary>
    /// StartUpScreenScene is the class responsible for the startup state of the gameplay, 
    /// i.e the slashscreen.
    /// </summary>
    class StartUpScreenScene : IGamescene
    {

        public List<int> entitiesInState
        {
            get; set;
        }
        public Timer timer;
        public bool timeOut;
        private FadeComponent fc;

        /// <summary>
        /// StartUpScene constructor, is responsible for setting the scene for the startup state of the gameplay
        /// </summary>
        public StartUpScreenScene()
        {
            //This function should create the needed enteties for the start up screen exemple add background, texture component, maybe add some text on the startup screen 
            //Create some help function if that is needed 

            entitiesInState = new List<int>();
            timeOut = true;
        }


        /// <summary>
        /// StartUpScreenState alternate constructor. Can be used when we want to define a exact time which we would remain in this state.
        /// </summary>
        /// <param name="time">takes a double which would represent the time which we want to remain in this state.</param>
        public StartUpScreenScene(double time)
        {
            entitiesInState = new List<int>();
            timer = new Timer(time);
            timer.Elapsed += timeElapsed;
            timer.Start();
            timeOut = false;
        }


        /// <summary>
        /// onSceneCreated this function is called whenever the current gamestate is changed. This function should contain logic that 
        /// needs to be processed before the state is shown for the player. This could be enteties that's not able to be created pre-runtime.
        /// </summary>
        public void onSceneCreated()
        {
            //add the enteties which should be displayed on the screen when the players choose their caracters. Player enteties is to be created after leaving this state therefore the
            //add entetiestolist - function needs to be called before entering the playing-state

            DrawableTextComponent dtx = new DrawableTextComponent("Flappy Ass, version 1.0 By: PT2", Color.Tomato, Game.Instance.GetContent<SpriteFont>("Fonts/MenuFont"));
            PositionComponent pc = new PositionComponent(new Vector2(20, 100));
            fc = new FadeComponent(254, -3);

            int id = ComponentManager.Instance.CreateID();

            ComponentManager.Instance.AddComponentToEntity(id, fc);
            ComponentManager.Instance.AddComponentToEntity(id, dtx);
            ComponentManager.Instance.AddComponentToEntity(id, pc);

            entitiesInState.Add(id);
        }

        /// <summary>
        /// onSceneUpdate this function handles the logic for the state which should be run durring the update partion of the game.
        /// For example this could be to check for conditions to continue to the next state of the gameplay. 
        /// </summary>
        public void onSceneUpdate()
        {
            if(fc.alphaValue <= 0)
            {
                List<IComponent> complist;
                //This is used for changin the currentState 
                //SetUpPlayerScene stateTwo = new SetUpPlayerScene();

                //SceneSystem.Instance.setCurrentScene(stateTwo);

                string[] menuItems = { "Start Game", "Options", "End Game" };

                MenuScene menu = new MenuScene(menuItems);
                SceneSystem.Instance.setCurrentScene(menu);

                foreach(int comp in entitiesInState)
                {
                    complist = ComponentManager.Instance.GetAllEntityComponents(comp);
                    foreach (IComponent a in complist)
                    {
                        if(a.GetType() == typeof(DrawableComponent))
                        {
                            DrawableComponent hej = (DrawableComponent)a;
                            hej.visable = false;

                        }
                        if(a.GetType() == typeof(DrawableTextComponent))
                        {
                            DrawableTextComponent hej = (DrawableTextComponent)a;
                            hej.visable = false;

                        }
                    }

                }
                
            }
        }
        /// <summary>
        /// timeElapsed function changes the timeOut variable to true enable to check if the enitial time has run out.
        /// </summary>
        /// <param name="source">is the object which fired the event</param>
        /// <param name="e">is the event arguments</param>
        private void timeElapsed(Object source, ElapsedEventArgs e)
        {
            timeOut = true;
        }

    }
}
