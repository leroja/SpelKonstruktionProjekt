using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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




        public DrawableTextComponent(string text, Color color, SpriteFont font)
        {
            this.text = text;
            this.textColor = color;
            this.font = font;
            visable = true;
        }
        public DrawableTextComponent(string []items, SpriteFont font)
        {
            menuItems = items;
            this.font = font;
            visable = true;
        }
        /// <summary>
        /// This Method returns the selected index of the menu.
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
            //Vi kanske behöver ha en metod här i som returnerar storleken på menyn, 
            //så att vi vet hur den skall placeras.
        }
    }
}
