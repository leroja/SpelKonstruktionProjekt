using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameEngine.Source.Systems;
using GameEngine.Source.Managers;
namespace GameEngine
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class ECSGameEngine : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // @TODO make it easy to set max fps or turn of of max fps
        /// <summary>
        /// 
        /// </summary>
        public ECSGameEngine()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1190;
            graphics.PreferredBackBufferHeight = 780;

            //Do not synch our Draw method with the Vertical Retrace of our monitor
            graphics.SynchronizeWithVerticalRetrace = false;
            //Do not Call our Update method at the default rate of 1/60 of a second.
            IsFixedTimeStep = false;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            SystemManager.Instance.AddSystem(new MouseSystem());
            SystemManager.Instance.AddSystem(new GamePadSystem());
            SystemManager.Instance.AddSystem(new _2DSpriteSystem());
            SystemManager.Instance.AddSystem(new TextRenderSystem());
            SystemManager.Instance.AddSystem(new KeyBoardSystem());
            SystemManager.Instance.AddSystem(new SoundEffectSystem());
            SystemManager.Instance.AddSystem(new AnimationSystem());
            SystemManager.Instance.AddSystem(SceneSystem.Instance);

            // @TODO make this system better
            SystemManager.Instance.AddSystem(new WindowTitleFPSSystem(this));


            // Test
            SystemManager.Instance.AddSystem(new TestDrawSystem(graphics));

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {


            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            SystemManager.Instance.spriteBatch = spriteBatch;

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
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            
            SystemManager.Instance.GameTime = gameTime;
            SystemManager.Instance.RunInputSystems();
            SystemManager.Instance.RunUpdateSystems();
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here
            SystemManager.Instance.GameTime = gameTime;
            SystemManager.Instance.RunRenderSystems();
            base.Draw(gameTime);
        }
    }
}
