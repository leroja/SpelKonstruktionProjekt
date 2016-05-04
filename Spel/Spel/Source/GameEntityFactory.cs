using GameEngine.Source.Components;
using GameEngine.Source.Enumerator;
using GameEngine.Source.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        /// Creates the rabit that is/was used for initial testing
        /// </summary>
        /// <param name="pixlePer"></param>
        /// <param name="up"></param>
        /// <param name="left"></param>
        /// <param name="down"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public int CreateTestKanin(bool pixlePer, Keys up, Keys left, Keys down, Keys right, Vector2 position)
        {

            DrawableComponent comp = new DrawableComponent(Game.Inst().GetContent<Texture2D>("Pic/Kanin"));
            PositionComponent pos = new PositionComponent(position);
            VelocityComponent vel = new VelocityComponent(new Vector2(200F, 200F), 50F, 1000F);
            KeyBoardComponent kbc = new KeyBoardComponent();
            CollisionRectangleComponent CRC = new CollisionRectangleComponent(new Rectangle((int)pos.position.X, (int)pos.position.Y, comp.texture.Width, comp.texture.Height));
            CollisionComponent CC = new CollisionComponent(pixlePer);
            kbc.keyBoardActions.Add(ActionsEnum.Up, up);
            kbc.keyBoardActions.Add(ActionsEnum.Down, down);
            kbc.keyBoardActions.Add(ActionsEnum.Left, left);
            kbc.keyBoardActions.Add(ActionsEnum.Right, right);

            int id = ComponentManager.Instance.CreateID();
            ComponentManager.Instance.AddComponentToEntity(id, vel);
            ComponentManager.Instance.AddComponentToEntity(id, comp);
            ComponentManager.Instance.AddComponentToEntity(id, pos);
            ComponentManager.Instance.AddComponentToEntity(id, kbc);
            ComponentManager.Instance.AddComponentToEntity(id, CRC);
            ComponentManager.Instance.AddComponentToEntity(id, CC);
           
            return id;
        }

        // @Todo Not finished yet
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public int CreateBorderRecs(Vector2 pos, int width, int height)
        {
            PositionComponent PC = new PositionComponent(pos);
            CollisionRectangleComponent CRC = new CollisionRectangleComponent(new Rectangle((int)pos.X, (int)pos.Y, width, height));
            CollisionComponent CC = new CollisionComponent(false);
            int id = ComponentManager.Instance.CreateID();
            ComponentManager.Instance.AddComponentToEntity(id, PC);
            ComponentManager.Instance.AddComponentToEntity(id, CRC);
            ComponentManager.Instance.AddComponentToEntity(id, CC);

            return id;
        }

    }
}
