using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameEngine.Source.Components;
using GameEngine.Source.Managers;
using GameEngine;
using Spel.Source.Systems;
using Microsoft.Xna.Framework.Audio;
using GameEngine.Source.Enumerator;

namespace Spel
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game : ECSGameEngine
    {
        public Game() : base()
        {

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
            DrawableComponent comp = new DrawableComponent(Content.Load<Texture2D>("Pic/Kanin"));
            PositionComponent pos = new PositionComponent(Vector2.Zero);
            VelocityComponent vel = new VelocityComponent(new Vector2(200F,200F), 50F, 1000F);
            KeyBoardComponent kbc = new KeyBoardComponent();
            CollisionRectangleComponent CRC = new CollisionRectangleComponent(new Rectangle((int)pos.position.X, (int)pos.position.Y, comp.texture.Width, comp.texture.Height));
            CollisionComponent CC = new CollisionComponent(true);
            SoundEffectComponent sfc = new SoundEffectComponent("Bouncy");
            kbc.keyBoardActions.Add(ActionsEnum.Up, Keys.Up);
            kbc.keyBoardActions.Add(ActionsEnum.Down, Keys.Down);
            kbc.keyBoardActions.Add(ActionsEnum.Left, Keys.Left);
            kbc.keyBoardActions.Add(ActionsEnum.Right, Keys.Right);

            int id = ComponentManager.Instance.CreateID();
            int ids = ComponentManager.Instance.CreateID();
            ComponentManager.Instance.AddComponentToEntity(id, vel);
            ComponentManager.Instance.AddComponentToEntity(id, comp);
            ComponentManager.Instance.AddComponentToEntity(id, pos);
            ComponentManager.Instance.AddComponentToEntity(id, kbc);
            ComponentManager.Instance.AddComponentToEntity(id, CRC);
            ComponentManager.Instance.AddComponentToEntity(id, CC);

            //ComponentManager.Instance.AddComponentToEntity(id, sfc);
            ComponentManager.Instance.AddComponentToEntity(ids, fps);

            DrawableComponent comp2 = new DrawableComponent(Content.Load<Texture2D>("Pic/Kanin"));
            PositionComponent pos2 = new PositionComponent(Vector2.Zero);
            VelocityComponent vel2 = new VelocityComponent(new Vector2(200F, 200F), 50F, 1000F);
            KeyBoardComponent kbc2 = new KeyBoardComponent();
            CollisionComponent CC2 = new CollisionComponent(true);
            CollisionRectangleComponent CRC2 = new CollisionRectangleComponent(new Rectangle((int)pos2.position.X, (int)pos2.position.Y, comp2.texture.Width, comp2.texture.Height));

            SoundEffectComponent sfc2 = new SoundEffectComponent("Bouncy");
            kbc2.keyBoardActions.Add(ActionsEnum.Up, Keys.W);
            kbc2.keyBoardActions.Add(ActionsEnum.Down, Keys.S);
            kbc2.keyBoardActions.Add(ActionsEnum.Left, Keys.A);
            kbc2.keyBoardActions.Add(ActionsEnum.Right, Keys.D);
            DrawableTextComponent dtc = new DrawableTextComponent("test Test test", Color.Brown, Content.Load<SpriteFont>("Fonts/TestFont"));
            MouseComponent mo = new MouseComponent();

            int id2 = ComponentManager.Instance.CreateID();
            ComponentManager.Instance.AddComponentToEntity(id2, vel2);
            ComponentManager.Instance.AddComponentToEntity(id2, comp2);
            ComponentManager.Instance.AddComponentToEntity(id2, pos2);
            ComponentManager.Instance.AddComponentToEntity(id2, kbc2);
            ComponentManager.Instance.AddComponentToEntity(id2, CRC2);
            ComponentManager.Instance.AddComponentToEntity(id2, CC2);
            ComponentManager.Instance.AddComponentToEntity(id2, dtc);
            ComponentManager.Instance.AddComponentToEntity(id2, mo);

            PositionComponent pos3 = new PositionComponent(Vector2.Zero); 
            CollisionRectangleComponent CRC3 = new CollisionRectangleComponent(new Rectangle((int)pos3.position.X, (int)pos3.position.Y, GraphicsDevice.Viewport.Width, 0));
            CollisionComponent CC3 = new CollisionComponent(false);
            int idtopwall3 = ComponentManager.Instance.CreateID();
            ComponentManager.Instance.AddComponentToEntity(idtopwall3, pos3);
            ComponentManager.Instance.AddComponentToEntity(idtopwall3, CRC3);
            ComponentManager.Instance.AddComponentToEntity(idtopwall3, CC3);

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


        //public static T get_content<T>(string asset) {
        //    return test<T>(asset);
        //}

        //private T test<T>(string asset)
        //{
        //    return (Content.Load<T>(asset));
        //}
    }
}
