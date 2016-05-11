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

namespace Spel.Menus
{
    public class MainMenu : IGamescene
    {
        DrawableTextComponent menuComp;
        Texture2D pic;
        Rectangle picRec;
        //@TODO: Fixa funktionalitet för entitiesInState;
        List<int> entitiesInState;

        public MainMenu(SpriteFont font, Texture2D pic, Color color)
        {
            string[] menuItems = { "Start Game", "Options", "End Game" };
            //DrawableTextComponent comp = new DrawableTextComponent(menuItems, font, color);
            //menuComp = comp;
            this.pic = pic;
            //@TODO: Fixa så att storleken på skärmen hämtas här.
            //picRec = new Rectangle(0, 0, Game.Window.ClientBounds.Width, Game.Window.ClientBounds.Height);
        }

        public MainMenu()
        {
            string[] menuItems = { "Start Game", "Options", "End Game" };
            entetiesInState = new List<int>();

            Dictionary<Vector2, String> menuList = new Dictionary<Vector2, string>();
            float x = GraphicsDeviceManager.DefaultBackBufferHeight / 2;
            float y = GraphicsDeviceManager.DefaultBackBufferWidth / 2;
            int i = 0;
            foreach (string a in menuItems)
            {
                menuList.Add(new Vector2(x, y), menuItems[i]);
                y += 30;
                i++;
            }
            DrawableTextComponent textcomp = new DrawableTextComponent(menuList, Game.Inst().GetContent<SpriteFont>("Fonts/MenuFont"), Color.Black);
            int id = ComponentManager.Instance.CreateID();
            ComponentManager.Instance.AddComponentToEntity(id, textcomp);
            entetiesInState.Add(id);
        }

        public List<int> entetiesInState
        {
            get; set;
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
            return;
        }

        public void onSceneUpdate()
        {
            return;
        }
    }
}
