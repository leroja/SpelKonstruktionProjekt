using GameEngine.Source.Components;
using GameEngine.Source.Enumerator;
using GameEngine.Source.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Spel.Source.Components;
using Spel.Source.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spel.Source
{
    /// <summary>
    /// A Factory for creating game related entities
    /// </summary>
    public class GameEntityFactory
    {
        private static GameEntityFactory instance;

        private GameEntityFactory()
        {
        }

        public static GameEntityFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameEntityFactory();
                }
                return instance;
            }
        }
        /// <summary>
        /// Creates an new Player with Controlls
        /// </summary>
        /// <param name="pixlePer"> True if pixelPerfect shall be used </param>
        /// <param name="GamePade"> True if GamePad the player uses a gamepad </param>
        /// <param name="PadJump"> Key binding to gamePad </param>
        /// <param name="Jump"> key binding to keybord </param>
        /// <param name="position"> Player start Position </param>
        /// <param name="name"> The name off the player</param>
        /// <param name="dir"> The players starting direction</param>
        /// <param name="index">  Playerindex For GamePad </param>
        /// <returns></returns>
        public int CreatePlayer(bool pixlePer, bool GamePade,Buttons PadJump, Keys Jump, Vector2 position, string name,Direction dir, PlayerIndex index, Color colour)
        {
            SpriteEffects flip;
            GamePadComponent gam;
            KeyBoardComponent kcb;
            int id = ComponentManager.Instance.CreateID();

            if (dir == Direction.Left)
            {
                flip = SpriteEffects.FlipHorizontally;
            }
            else
            {
                flip = SpriteEffects.None;
            }

            if (GamePade == true)
            {
                gam = new GamePadComponent(index);
                gam.gamepadActions.Add(ActionsEnum.Jump, PadJump);
                ComponentManager.Instance.AddComponentToEntity(id, gam);
            }
            else
            {
                kcb = new KeyBoardComponent();
                kcb.keyBoardActions.Add(ActionsEnum.Jump, Jump);
                ComponentManager.Instance.AddComponentToEntity(id, kcb);
            }
            DirectionComponent dc = new DirectionComponent(dir);
            DrawableComponent comp = new DrawableComponent(Game.Instance.GetContent<Texture2D>("Pic/kanin1"), flip);
            PositionComponent pos = new PositionComponent(position);
            VelocityComponent vel = new VelocityComponent(new Vector2(200F, 0), 50F);
            JumpComponent jump = new JumpComponent(300F, 200F);
            CollisionRectangleComponent CRC = new CollisionRectangleComponent(new Rectangle((int)pos.position.X, (int)pos.position.Y, comp.texture.Width, comp.texture.Height));
            CollisionComponent CC = new CollisionComponent(pixlePer);
            PlayerComponent pc = new PlayerComponent(name);
            DrawableTextComponent dtc = new DrawableTextComponent(name, Color.Black, Game.Instance.GetContent<SpriteFont>("Fonts/TestFont"));
            HUDComponent hudc = new HUDComponent(Game.Instance.GetContent<Texture2D>("Pic/PowerUp"), new Vector2(pos.position.X, pos.position.Y));
            HUDComponent hudc2 = new HUDComponent(Game.Instance.GetContent<Texture2D>("Pic/PowerUp"), Vector2.One);
            HealthComponent hc = new HealthComponent(3, false);
            //AnimationComponent ani = new AnimationComponent(100, 114, comp.texture.Width, comp.texture.Height, 0.2);

            comp.colour = colour;
            
            ComponentManager.Instance.AddComponentToEntity(id, vel);
            ComponentManager.Instance.AddComponentToEntity(id, comp);
            ComponentManager.Instance.AddComponentToEntity(id, pos);
            ComponentManager.Instance.AddComponentToEntity(id, CRC);
            ComponentManager.Instance.AddComponentToEntity(id, CC);
            ComponentManager.Instance.AddComponentToEntity(id, pc);
            ComponentManager.Instance.AddComponentToEntity(id, dtc);
            ComponentManager.Instance.AddComponentToEntity(id, hudc);
            ComponentManager.Instance.AddComponentToEntity(id, hc);
            //ComponentManager.Instance.AddComponentToEntity(id, ani);
            ComponentManager.Instance.AddComponentToEntity(id, dc);
            ComponentManager.Instance.AddComponentToEntity(id, jump);
            return id;
        }

        /// <summary>
        /// Creates a border rectangle
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="side"></param>
        /// <returns></returns>
        public int CreateBorderRecs(Vector2 pos, int width, int height, Wall side)
        {
            PositionComponent PC = new PositionComponent(pos);
            CollisionRectangleComponent CRC = new CollisionRectangleComponent(new Rectangle((int)pos.X, (int)pos.Y, width, height));
            CollisionComponent CC = new CollisionComponent(false);
            WallComponent wc = new WallComponent(side);
            int id = ComponentManager.Instance.CreateID();
            ComponentManager.Instance.AddComponentToEntity(id, PC);
            ComponentManager.Instance.AddComponentToEntity(id, CRC);
            ComponentManager.Instance.AddComponentToEntity(id, CC);
            ComponentManager.Instance.AddComponentToEntity(id, wc);

            return id;
        }
        /// <summary>
        /// Creates an new PowerUpp
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public int CreateTestPowerUp(Vector2 position)
        {
            Random rand = new Random();
            PositionComponent Pc = new PositionComponent(position);
            PowerUppComponent power = new PowerUppComponent(rand.Next(1,3));
            DrawableComponent powerupp = new DrawableComponent(Game.Instance.GetContent<Texture2D>("Pic/Powerupbox"),SpriteEffects.None);
            CollisionRectangleComponent CRC = new CollisionRectangleComponent(new Rectangle((int)position.X, (int)position.Y, powerupp.texture.Width, powerupp.texture.Height));
            CollisionComponent CC = new CollisionComponent(true);
            int id = ComponentManager.Instance.CreateID();
            ComponentManager.Instance.AddComponentToEntity(id, CRC);
            ComponentManager.Instance.AddComponentToEntity(id, CC);
            ComponentManager.Instance.AddComponentToEntity(id, Pc);
            ComponentManager.Instance.AddComponentToEntity(id, power);
            ComponentManager.Instance.AddComponentToEntity(id, powerupp);
            return id;
        }

        /// <summary>
        /// Creates a platform at the specified position with the specified lenght, width and Texture;
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="texture"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns> the id of the created platform identity </returns>
        public int CreatePlatform(Vector2 pos, string texture, int width, int height)
        {
            PositionComponent Pc = new PositionComponent(pos);
            DrawableComponent DC = new DrawableComponent(Game.Instance.GetContent<Texture2D>("Pic/"+texture),SpriteEffects.None);
            CollisionRectangleComponent CRC = new CollisionRectangleComponent(new Rectangle((int)pos.X, (int)pos.Y, width, height));
            CollisionComponent CC = new CollisionComponent(false);
            PlatformComponent Plc = new PlatformComponent(pos, width, height);
            Plc.RedoRecs(pos, DC.texture.Width, DC.texture.Height);

            int id = ComponentManager.Instance.CreateID();


            ComponentManager.Instance.AddComponentToEntity(id, CRC);
            ComponentManager.Instance.AddComponentToEntity(id, CC);
            ComponentManager.Instance.AddComponentToEntity(id, Pc);
            ComponentManager.Instance.AddComponentToEntity(id, DC);
            ComponentManager.Instance.AddComponentToEntity(id, Plc);
            return id;
        }

        public int CreateChangeCube(Vector2 position, string texture, int width, int height)
        {
            int id = ComponentManager.Instance.CreateID();
            ChangeCubeComponent cdc = new ChangeCubeComponent();
            PositionComponent pos = new PositionComponent(position);
            CollisionComponent col = new CollisionComponent(false);
            CollisionRectangleComponent colRec = new CollisionRectangleComponent(new Rectangle((int)position.X, (int)position.Y, width, height));
            DrawableComponent draw = new DrawableComponent(Game.Instance.GetContent<Texture2D>(texture),SpriteEffects.None);
            AnimationComponent ani = new AnimationComponent(50, 50, draw.texture.Width, draw.texture.Height, 0.08f);
            ani.oneTime = true;

            ComponentManager.Instance.AddComponentToEntity(id, cdc);
            ComponentManager.Instance.AddComponentToEntity(id, pos);
            ComponentManager.Instance.AddComponentToEntity(id, col);
            ComponentManager.Instance.AddComponentToEntity(id, colRec);
            ComponentManager.Instance.AddComponentToEntity(id, draw);
            ComponentManager.Instance.AddComponentToEntity(id, ani);

            return id;
        }

        public int CreateBackground(Vector2 position, string texture, int width, int height)
        {
            int id = ComponentManager.Instance.CreateID();
            PositionComponent pos = new PositionComponent(position);
            DrawableComponent draw = new DrawableComponent(Game.Instance.GetContent<Texture2D>("Background/" + texture), SpriteEffects.None);
            ComponentManager.Instance.AddComponentToEntity(id, pos);
            ComponentManager.Instance.AddComponentToEntity(id, draw);

            return id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="position"></param>
        /// <param name="pixlePer"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public int CreateAIPlayer(Direction dir, Vector2 position, bool pixlePer, string name, Color colour)
        {
            SpriteEffects flip;

            if (dir == Direction.Left)
            {
                flip = SpriteEffects.FlipHorizontally;
            }
            else
            {
                flip = SpriteEffects.None;
            }

            DirectionComponent dc = new DirectionComponent(dir);
            DrawableComponent comp = new DrawableComponent(Game.Instance.GetContent<Texture2D>("Pic/Helmutsh"),flip);
            PositionComponent pos = new PositionComponent(position);
            VelocityComponent vel = new VelocityComponent(new Vector2(200F, 0), 50F);
            JumpComponent jump = new JumpComponent(300F, 50F);
            CollisionRectangleComponent CRC = new CollisionRectangleComponent(new Rectangle((int)pos.position.X, (int)pos.position.Y, comp.texture.Width, comp.texture.Height));
            CollisionComponent CC = new CollisionComponent(pixlePer);
            DrawableTextComponent dtc = new DrawableTextComponent(name, Color.Yellow, Game.Instance.GetContent<SpriteFont>("Fonts/NewTestFont"));
            HUDComponent hudc = new HUDComponent(Game.Instance.GetContent<Texture2D>("Pic/PowerUp"), new Vector2(pos.position.X, pos.position.Y));
            HUDComponent hudc2 = new HUDComponent(Game.Instance.GetContent<Texture2D>("Pic/PowerUp"), Vector2.One);
            HealthComponent hc = new HealthComponent(3, false);
            AnimationComponent ani = new AnimationComponent(75, 75, comp.texture.Width, comp.texture.Height, 0.2);
            AIComponent ai = new AIComponent();
            PlayerComponent play = new PlayerComponent(name);

            comp.colour = colour;

            int id = ComponentManager.Instance.CreateID();
            ComponentManager.Instance.AddComponentToEntity(id, vel);
            ComponentManager.Instance.AddComponentToEntity(id, comp);
            ComponentManager.Instance.AddComponentToEntity(id, pos);
            ComponentManager.Instance.AddComponentToEntity(id, CRC);
            ComponentManager.Instance.AddComponentToEntity(id, CC);
            ComponentManager.Instance.AddComponentToEntity(id, dtc);
            ComponentManager.Instance.AddComponentToEntity(id, hudc);
            ComponentManager.Instance.AddComponentToEntity(id, hc);
            ComponentManager.Instance.AddComponentToEntity(id, ani);
            ComponentManager.Instance.AddComponentToEntity(id, dc);
            ComponentManager.Instance.AddComponentToEntity(id, jump);
            ComponentManager.Instance.AddComponentToEntity(id, play);
            ComponentManager.Instance.AddComponentToEntity(id, ai);
            return id;
        }
    }
}
