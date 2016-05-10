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

namespace Spel.Menus
{
    public class MainMenu : IGamescene
    {
        DrawableTextComponent menuComp;
        Texture2D pic;
        Rectangle picRec;
        //@TODO: Fixa funktionalitet för entitiesInState;
        List<int> entitiesInState;

        public MainMenu(SpriteBatch spriteBatch, SpriteFont font, Texture2D pic)
        {
            string[] menuItems = { "Start Game", "Options", "End Game" };
            DrawableTextComponent comp = new DrawableTextComponent(menuItems, font);
            menuComp = comp;
            this.pic = pic;
            //@TODO: Fixa så att storleken på skärmen hämtas här.
            //picRec = new Rectangle(0, 0, Game.Window.ClientBounds.Width, Game.Window.ClientBounds.Height);
        }

        public List<int> entetiesInState
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }
        public void show()
        {

        }
        public void hide()
        {

        }
        //@TODO: Fixa dessa funktioner för att hantera menyns funktionalitet.
        public void onSceneCreated()
        {
            throw new NotImplementedException();
        }

        public void onSceneUpdate()
        {
            throw new NotImplementedException();
        }
    }
}
