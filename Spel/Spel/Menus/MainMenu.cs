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

namespace Spel.Menus
{
    public class MainMenu : GameScene
    {
        MenuComponent menuComp;
        Texture2D pic;
        Rectangle picRec;

        public MainMenu(Game game, SpriteBatch spritebatch, SpriteFont font, Texture2D pic) : base(game, spritebatch)
        {
            string[] menuItems = { "Start Game", "Options", "End Game" };
            MenuComponent comp = new MenuComponent(game, spriteBatch, font, menuItems);
            Components.Add(comp);
            this.pic = pic;
            picRec = new Rectangle(0, 0, Game.Window.ClientBounds.Width, Game.Window.ClientBounds.Height);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(pic, picRec, Color.WhiteSmoke);
            base.Draw(gameTime);
        }
    }
}
