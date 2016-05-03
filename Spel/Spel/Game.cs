using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameEngine.Source.Components;
using GameEngine.Source.Managers;
using GameEngine;
using Spel.Source.Systems;
using Microsoft.Xna.Framework.Audio;
using GameEngine.Source.Enumerator;
using Spel.Source;

namespace Spel
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game : ECSGameEngine
    {

        private static Game instance;

        public Game() : base()
        {
            instance = this;
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
            int ids = ComponentManager.Instance.CreateID();
            ComponentManager.Instance.AddComponentToEntity(ids, fps);

            GameEntityFactory.Instance.CreateTestKanin(true, Keys.Up, Keys.Left, Keys.Down, Keys.Right);
            GameEntityFactory.Instance.CreateTestKanin(true, Keys.W, Keys.A, Keys.S, Keys.D);

            GameEntityFactory.Instance.CreateBorderRecs(Vector2.Zero, GraphicsDevice.Viewport.Width, 0);
            GameEntityFactory.Instance.CreateBorderRecs(Vector2.Zero, 0, GraphicsDevice.Viewport.Height);
            GameEntityFactory.Instance.CreateBorderRecs(new Vector2(0, GraphicsDevice.Viewport.Height), GraphicsDevice.Viewport.Width, 0);
            GameEntityFactory.Instance.CreateBorderRecs(new Vector2(GraphicsDevice.Viewport.Width, 0), 0, GraphicsDevice.Viewport.Height);

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
            
            base.LoadContent();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }



        public static Game Inst()
        {
            return instance;
        }

        public T GetContent<T>(string asset)
        {
            return (Content.Load<T>(asset));

        }
    }
}
