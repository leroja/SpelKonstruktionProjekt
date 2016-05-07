﻿using GameEngine.Source.Components;
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
        /// Creates the rabit that is/was used for initial testing
        /// </summary>
        /// <param name="pixlePer"></param>
        /// <param name="up"></param>
        /// <param name="left"></param>
        /// <param name="down"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public int CreateTestKanin(bool pixlePer, Keys up, Keys left, Keys down, Keys right, Vector2 position, string name)
        {


            DrawableComponent comp = new DrawableComponent(Game.Inst().GetContent<Texture2D>("Pic/Kanin1"));
            PositionComponent pos = new PositionComponent(position);
            VelocityComponent vel = new VelocityComponent(new Vector2(200F, 200F), 50F, 500F);
            KeyBoardComponent kbc = new KeyBoardComponent();
            CollisionRectangleComponent CRC = new CollisionRectangleComponent(new Rectangle((int)pos.position.X, (int)pos.position.Y, comp.texture.Width, comp.texture.Height));
            CollisionComponent CC = new CollisionComponent(pixlePer);
            PlayerComponent pc = new PlayerComponent(name);
            DrawableTextComponent dtc = new DrawableTextComponent(name, Color.BurlyWood, Game.Inst().GetContent<SpriteFont>("Fonts/TestFont"));
            HUDComponent hudc = new HUDComponent(Game.Inst().GetContent<Texture2D>("Pic/PowerUp"));
            HealthComponent hc = new HealthComponent(3);
            hc.health = 3;
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
            ComponentManager.Instance.AddComponentToEntity(id, pc);
            ComponentManager.Instance.AddComponentToEntity(id, dtc);
            ComponentManager.Instance.AddComponentToEntity(id, hudc);
            ComponentManager.Instance.AddComponentToEntity(id, hc);

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
        public int CreateBorderRecs(Vector2 pos, int width, int height, Wall wall)
        {
            PositionComponent PC = new PositionComponent(pos);
            CollisionRectangleComponent CRC = new CollisionRectangleComponent(new Rectangle((int)pos.X, (int)pos.Y, width, height));
            CollisionComponent CC = new CollisionComponent(false);
            WallComponent wc = new WallComponent(wall);
            int id = ComponentManager.Instance.CreateID();
            ComponentManager.Instance.AddComponentToEntity(id, PC);
            ComponentManager.Instance.AddComponentToEntity(id, CRC);
            ComponentManager.Instance.AddComponentToEntity(id, CC);
            ComponentManager.Instance.AddComponentToEntity(id, wc);

            return id;
        }
        public int CreateTestPowerUp(Vector2 position)
        {
            PositionComponent Pc = new PositionComponent(position);
            PowerUppComponent power = new PowerUppComponent(1);
            DrawableComponent powerupp = new DrawableComponent(Game.Inst().GetContent<Texture2D>("Pic/PowerUp"));
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
    }
}
