using GameEngine.Source.Components;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Spel.Menus
{
    /// <summary>
    /// This class is used to but a menu into a scene which will be used to display the menu within the game.
    /// </summary>
    public abstract class GameScene : Microsoft.Xna.Framework.DrawableGameComponent
    {
        List<MenuComponent> components = new List<MenuComponent>();
        protected Game game;
        protected SpriteBatch spriteBatch;
        /// <summary>
        /// This method returns the components that is contained within the menu.
        /// </summary>
        public List<MenuComponent> Components
        {
            get { return components; }
        }
        /// <summary>
        /// The constructor for the GameScene.
        /// </summary>
        /// <param name="game">
        /// The game in which the menu is contained.
        /// </param>
        /// <param name="spriteBatch">TA spritebatch to handle the graphical elements of the menu.
        /// </param>
        public GameScene(Game game, SpriteBatch spriteBatch) : base(game)
        {
            this.game = game;
            this.spriteBatch = spriteBatch;
        }
        /// <summary>
        /// Initialize method inherited from DrawableGameComponent.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
        }
        /// <summary>
        /// Update method inherited from DrawableGameComponent.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            foreach (MenuComponent comp in components)
            {
                //if (comp.Enabled == true)
                //    comp.Update(GameTime);
            }
        }
        /// <summary>
        /// Draw method inherited from DrawableGameComponent.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            foreach(IComponent comp in components)
            {
                if(comp is DrawableComponent && ((DrawableGameComponent)comp).Visible)
                    ((DrawableGameComponent)comp).Draw(gameTime);
            }
        }
        /// <summary>
        /// This method is used to show the menu when the state of the game changes to the specified menu.
        /// </summary>
        public virtual void show()
        {
            this.Visible = true;
            this.Enabled = true;

            foreach(IComponent comp in components)
            {
                //comp.Enabled = true;
                if (comp is DrawableGameComponent)
                    ((DrawableGameComponent)comp).Visible = true;
            }
        }
        /// <summary>
        /// This method is used to hide the specified menu.
        /// </summary>
        public virtual void hide()
        {
            this.Visible = false;
            this.Enabled = false;

            foreach (IComponent comp in components)
            {
                //comp.Enabled = false;
                if (comp is DrawableGameComponent)
                    ((DrawableGameComponent)comp).Visible = false;
            }
        }
    }
}
