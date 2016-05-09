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

namespace Spel.Source.Gamestates
{
    class StartUpScreenState : IGamestate
    {

        public List<int> entetiesInState
        {
            get; set;
        }
        public Timer timer;
        public bool timeOut;
        public bool professor;

        /// <summary>
        /// StartUpState constructor, is responsible for setting the scene for the startup state of the gameplay
        /// </summary>
        public StartUpScreenState()
        {
            //This function should create the needed enteties for the start up screen exemple add background, texture component, maybe add some text on the startup screen 
            //Create some help function if that is needed 

            entetiesInState = new List<int>();
            timeOut = true;
        }


        /// <summary>
        /// StartUpScreenState alternate constructor. Can be used when we want to define a exact time which we would remain in this state.
        /// </summary>
        /// <param name="time">takes a double which would represent the time which we want to remain in this state.</param>
        public StartUpScreenState(double time)
        {
            professor = false;
            entetiesInState = new List<int>();
            timer = new Timer(time);
            timer.Elapsed += timeElapsed;
            timer.Start();
            timeOut = false;
        }
       

        /// <summary>
        /// InitializeState method is used for initializing the enteties/object which is needed in the startupscreen.
        /// </summary>
        public void onSceneCreated()
        {
            //add the enteties which should be displayed on the screen when the players choose their caracters. Player enteties is to be created after leaving this state therefore the
            //add entetiestolist - function needs to be called before entering the playing-state

            DrawableTextComponent dtx = new DrawableTextComponent("Flappy Ass, version 1.0 By: PT2", Color.Tomato, Game.Inst().GetContent<SpriteFont>("Fonts/MenuFont"));
            PositionComponent pc = new PositionComponent(new Vector2(20, 100));

            int id = ComponentManager.Instance.CreateID();

            ComponentManager.Instance.AddComponentToEntity(id, dtx);
            ComponentManager.Instance.AddComponentToEntity(id, pc);

            entetiesInState.Add(id);
        }
        public void onSceneUpdate()
        {
            if(timeOut == true)
            {
                //This is used for changin the currentState 
                SetUpPlayerState stateTwo = new SetUpPlayerState();
                
                SceneManager.Instance.setCurrentScene(stateTwo);
                
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
