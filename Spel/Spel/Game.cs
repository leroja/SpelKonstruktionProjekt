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
using Spel.Source.Enum;
using Spel.Source.Gamestates;
using GameEngine.Source.Systems;

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
            CollisionDetectionSystem det = new CollisionDetectionSystem();
            CollisionSystem col = new CollisionSystem();
            det.Subscribe(col);

            SystemManager.Instance.AddSystem(col);
            SystemManager.Instance.AddSystem(new HUDSystem());
            SystemManager.Instance.AddSystem(new HealthSystem());
            SystemManager.Instance.AddSystem(det);
            SystemManager.Instance.AddSystem(new MovementSystem());
            SystemManager.Instance.AddSystem(new BallOfSpikesSystem());
            SystemManager.Instance.AddSystem(new SpawnPowerUpSystem(10));
            FPSCounterComponent fps = new FPSCounterComponent();
            int ids = ComponentManager.Instance.CreateID();
            ComponentManager.Instance.AddComponentToEntity(ids, fps);

            GameEntityFactory.Instance.CreateTestKanin(true, Keys.Up, new Vector2(GraphicsDevice.Viewport.Width / 2, 10), "Kanin 1");
            GameEntityFactory.Instance.CreateTestKanin(true, Keys.W, Vector2.One, "Kanin 2");
            GameEntityFactory.Instance.CreateBorderRecs(Vector2.Zero, GraphicsDevice.Viewport.Width, 0, Wall.TopWall);
            GameEntityFactory.Instance.CreateBorderRecs(Vector2.Zero, 0, GraphicsDevice.Viewport.Height, Wall.LeftWall);
            GameEntityFactory.Instance.CreateBorderRecs(new Vector2(0, GraphicsDevice.Viewport.Height), GraphicsDevice.Viewport.Width, 0, Wall.BottomWall);
            GameEntityFactory.Instance.CreateBorderRecs(new Vector2(GraphicsDevice.Viewport.Width, 0), 0, GraphicsDevice.Viewport.Height, Wall.RightWall);

            


            //StartUpScreenScene stateOne = new StartUpScreenScene(1000);
            //SceneSystem.Instance.setCurrentScene(stateOne);



            //GameEntityFactory.Instance.CreateTestPowerUp(new Vector2(100,400));

            GameEntityFactory.Instance.CreatePlatform(new Vector2(200, 250), "suddis", 150, 20);
            GameEntityFactory.Instance.CreatePlatform(new Vector2(800, 500), "suddis", 150, 20);



            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {

            AudioManager.Instance.AddSoundEffect("bouncy", Content.Load<SoundEffect>("Sound/Bouncy_Bounce-Bugs_Bunny-1735935456"));
            AudioManager.Instance.AddSoundEffect("jump", Content.Load<SoundEffect>("Sound/Jump"));
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
