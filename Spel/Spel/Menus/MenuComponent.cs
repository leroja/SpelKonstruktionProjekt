using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spel.Menus
{
    /// <summary>
    /// This class is used to store the content of a menu, that will be used within the game menus.
    /// </summary>
    public class MenuComponent : Microsoft.Xna.Framework.DrawableGameComponent
    {
        string[] menuItems;
        int selectedItem;

        Color standard = Color.Crimson;
        Color highlight = Color.SpringGreen;

        KeyboardState prevState;
        KeyboardState currentState;

        SpriteBatch spritebatch;
        SpriteFont font;

        Vector2 position;
        float width = 0f;
        float height = 0f;

        /// <summary>
        /// This is the constructor for the menu component.
        /// </summary>
        /// <param name="game">
        /// The game which the menu will be bound to.
        /// </param>
        /// <param name="spriteBatch">
        /// A spritebatch that will be used to handle the graphical elements of the menu.
        /// </param>
        /// <param name="font">
        /// The font that will be used for the menus text content.
        /// </param>
        /// <param name="menuItems">
        /// The items that are going to be contained in the menu.
        /// </param>
        public MenuComponent(Game game, SpriteBatch spriteBatch, SpriteFont font, string[] menuItems) : base(game)
        {
            this.spritebatch = spriteBatch;
            this.font = font;
            this.menuItems = menuItems;
            getMenuSize();
        }

        /// <summary>
        /// Functions to get and set the index of the selected menu property.
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
        /// <summary>
        /// This function returns the size that the menu will use. This is done to fit the menus to the screen size.
        /// </summary>
        private void getMenuSize()
        {
            height = 0;
            width = 0;
            
            foreach (string item in menuItems)
            {
                Vector2 size = font.MeasureString(item);
                
                if (size.X > width)
                    width = size.X;
                    height += font.LineSpacing + 5;
               
            }
            position = new Vector2(
                (Game.Window.ClientBounds.Width - width) / 2,
                (Game.Window.ClientBounds.Height - height) / 2);

        }
        /// <summary>
        /// Checks wheter or not a key is pressed.
        /// </summary>
        /// <param name="key">
        /// The key that will be controlled.
        /// </param>
        /// <returns>
        /// Returns true if the key is pressed, returns false if it isn't
        /// </returns>
        private bool controlKeys(Keys key)
        {
            return currentState.IsKeyUp(key) && prevState.IsKeyDown(key);
        }
        /// <summary>
        /// Initalize method inherited from DrawableGameComponent.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
        }
        /// <summary>
        /// Draw Method inherited from DrawableGameComponent.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            Vector2 location = position;
            Color tint;

            for (int i = 0; i < menuItems.Length; i++)
            {
                if (i == selectedIndex)
                    tint = highlight;
                else
                {
                    tint = standard;
                    spritebatch.DrawString(
                        font,
                        menuItems[i],
                        location,
                        tint);
                    location.Y += font.LineSpacing + 8;
                }
            }
        }
        /// <summary>
        /// Update method inherited from DrawableGameComponent.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            currentState = Keyboard.GetState();

            if (controlKeys(Keys.Down))
            {
                selectedIndex++;
                if (selectedIndex == menuItems.Length)
                    selectedIndex = 0;
            }
            base.Update(gameTime);

            prevState = currentState;
        }
    }
}
