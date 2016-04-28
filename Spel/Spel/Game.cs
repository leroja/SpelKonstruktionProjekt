using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameEngine.Source.Components;
using GameEngine.Source.Managers;
using GameEngine;
using Spel.Source.Systems;
using Microsoft.Xna.Framework.Audio;

namespace Spel
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game : ECSGameEngine// Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game() : base()
        {
            //graphics = new GraphicsDeviceManager(this);
            //Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {

            SystemManager.Instance.AddSystem(new MovementSystem());
            FPSCounterComponent fps = new FPSCounterComponent();
            // TODO: Add your initialization logic here
            DrawableComponent comp = new DrawableComponent(Content.Load<Texture2D>("Pic/Kanin"));
            PositionComponent pos = new PositionComponent(Vector2.Zero);
            VelocityComponent vel = new VelocityComponent(Vector2.One, 50F, 1000F);
            KeyBoardComponent kbc = new KeyBoardComponent();
            //SoundEffectComponent sfc = new SoundEffectComponent("Bouncy");
            kbc.keyBoardActions.Add("Up", Keys.Up);
            kbc.keyBoardActions.Add("Down", Keys.Down);
            kbc.keyBoardActions.Add("Left", Keys.Left);
            kbc.keyBoardActions.Add("Right", Keys.Right);

            int id = ComponentManager.Instance.CreateID();
            int ids = ComponentManager.Instance.CreateID();
            ComponentManager.Instance.AddComponentToEntity(id, vel);
            ComponentManager.Instance.AddComponentToEntity(id, comp);
            ComponentManager.Instance.AddComponentToEntity(id, pos);
            ComponentManager.Instance.AddComponentToEntity(id, kbc);
            //ComponentManager.Instance.AddComponentToEntity(id, sfc);
            ComponentManager.Instance.AddComponentToEntity(ids, fps);
            base.Initialize();

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            SoundEffect se = Content.Load<SoundEffect>("Sound/Bouncy_Bounce-Bugs_Bunny-1735935456");

            AudioManager.Instance.AddSoundEffect("Bouncy", se);
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            base.LoadContent();
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        //protected override void Update(GameTime gameTime)
        //{
        //    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        //        Exit();

        //    // TODO: Add your update logic here

        //    base.Update(gameTime);
        //}

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        //protected override void Draw(GameTime gameTime)
        //{
        //    GraphicsDevice.Clear(Color.CornflowerBlue);

        //    // TODO: Add your drawing code here

        //    base.Draw(gameTime);
        //}
    }
}
