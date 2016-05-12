using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Source.Components
{
    /// <summary>
    /// DrawableTextComponent is the class representing a text sprite,
    /// which is desired to be displayed on the screen.
    /// </summary>
    public class DrawableTextComponent : IComponent
    {
        public string text { get; set; }
        private string []menuItems { get; set; }
        public Color textColor { get; set; }
        public SpriteFont font { get; set; }
        public bool visable { get; set; }
        private Dictionary<Vector2,String> menu;



        /// <summary>
        /// Default constructor for a DrawableTextComponent
        /// </summary>
        /// <param name="text">takes a string which is the text that is needed to be displayed</param>
        /// <param name="color">takes a Color object which is the desired color of the text</param>
        /// <param name="font">takes a SpriteFont object which is the desired font of the text</param>
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
        /// The Dictionary that hold the text strings
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
        /// <summary>
        /// getMenuList method is used to access the dictionary stored within the component
        /// </summary>
        /// <returns>Returns a dictionary storing the positions and the strings for the meny items.</returns>
        public Dictionary<Vector2,string> getMenuList()
        {
            return menu;
        }
        /// <summary>
        /// addMenuItem function, could be used to add another item to the
        /// existing dictionary.
        /// </summary>
        /// <param name="position">a Vector2 containing the positions of the menu item.</param>
        /// <param name="menuName">a string which is the text for the menu item.</param>
        public void addMenuItem(Vector2 position, string menuName)
        {
            menu.Add(position, menuName);
        }
        /// <summary>
        /// removeMenuItem function, could be used to remove a item from the menu
        /// </summary>
        /// <param name="position">takes a Vector2 which is the position of the item which is desired to be removed.</param>
        public void removeMenuItem(Vector2 position)
        {
            menu.Remove(position);
        }
        /// <summary>
        /// getStringFromIndex method is used to retrive just a string in the dictionary
        /// </summary>
        /// <param name="index">takes an int index which should represent the place in the dictionary where the desired string is stored.</param>
        /// <returns>returns the string which is placed on this index in the dictionary</returns>
        public string getStringFromIndex(int index)
        {
            var pair = menu.ElementAt(index);
            string oneString = pair.Value;
            return oneString;

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
            //Vi kanske behöver ha en metod här i som returnerar storleken på menyn, 
            //så att vi vet hur den skall placeras.
        }
    }
}
