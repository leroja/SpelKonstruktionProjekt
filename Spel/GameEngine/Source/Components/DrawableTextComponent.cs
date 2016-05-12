using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Source.Components
{
    public class DrawableTextComponent : IComponent
    {
        public string text { get; set; }
        public string []menuItems { get; set; }
        public Color textColor { get; set; }
        public SpriteFont font { get; set; }
        public bool visable { get; set; }
        private Dictionary<Vector2,String> menu;
        KeyboardState prevState;
        KeyboardState currentState;





        public DrawableTextComponent(string text, Color color, SpriteFont font)
        {
            this.text = text;
            this.textColor = color;
            this.font = font;
            visable = true;
        }
        /// <summary>
        /// Secondary constructor that takes several text strings as an argument.
        /// </summary>
        /// <param name="items">
        /// The array that hold the text strings
        /// </param>
        /// <param name="font">
        /// To font to be used for the textcompoenent
        /// </param>
        /// <param name="color">
        /// The color to be used for the textcomponent
        /// </param>
        public DrawableTextComponent(Dictionary<Vector2,string> menuList, SpriteFont font, Color color)
        {
            textColor = color;
            //menuItems = items;
            this.font = font;
            visable = true;
            menu = menuList;
        }

        public Dictionary<Vector2,string> getMenuList()
        {
            return menu;
        }
        public void addMenuItem(Vector2 position, string menuName)
        {
            menu.Add(position, menuName);
        }
        public void removeMentuItem(Vector2 position)
        {
            menu.Remove(position);
        }
        /// <summary>
        /// This Method returns the selected index of a menu.
        /// </summary>
        public int selectedIndex
        {
            get { return selectedIndex; }
            set
            {
                selectedIndex = value;
                if (selectedIndex < 0)
                    selectedIndex = 0;
                if (selectedIndex >= menuItems.Length)
                    selectedIndex = menuItems.Length - 1;
            }
        }

        public bool controlKeys(Keys key)
        {
            return currentState.IsKeyUp(key) && prevState.IsKeyDown(key);
        }
    }
}
