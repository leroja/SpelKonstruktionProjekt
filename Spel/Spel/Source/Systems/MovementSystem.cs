using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Source.Components;
using GameEngine.Source.Managers;
using GameEngine.Source.Systems;
using GameEngine.Source.Systems.Interfaces;
using GameEngine.Source.Enumerator;
using Spel.Source.Components;
using Spel.Source.Enum;
using Microsoft.Xna.Framework.Graphics;

namespace Spel.Source.Systems
{
    /// <summary>
    /// A system for handling movement of players
    /// and other entities that require movement
    /// gravition is also handled in this system
    /// </summary>
    public class MovementSystem : IUpdate
    {
        private const float gravity = 500F;
        private const float sideMovement = 200F;
        public void update(GameTime gameTime)
        {
            List<int> dra = ComponentManager.Instance.GetAllEntitiesWithComponentType<PlayerComponent>();
            List<int> nonPlayerEntities = ComponentManager.Instance.GetAllEntitiesWithComponentType<MovementComponent>();

            if (nonPlayerEntities != null)
            {
                foreach (var entity in nonPlayerEntities)
                {
                    PositionComponent pos = ComponentManager.Instance.GetEntityComponent<PositionComponent>(entity);
                    MovementComponent move = ComponentManager.Instance.GetEntityComponent<MovementComponent>(entity);
                    pos.prevPosition = pos.position;

                    pos.position += move.movement;
                }
            }

            if (dra != null)
            {
                foreach (var a in dra)
                {
                    if (ComponentManager.Instance.CheckIfEntityHasComponent<KeyBoardComponent>(a))
                    {
                        KeyBoardMove(a, gameTime);
                    }
                    else
                    {
                        GamePadeMove(a, gameTime);
                    }
                    flip(a);
                }
            }
        }
        private void KeyBoardMove(int Id,GameTime gameTime)
        {
            KeyBoardComponent kbc = ComponentManager.Instance.GetEntityComponent<KeyBoardComponent>(Id);
            PositionComponent p = ComponentManager.Instance.GetEntityComponent<PositionComponent>(Id);
            PlayerComponent pc = ComponentManager.Instance.GetEntityComponent<PlayerComponent>(Id);
            VelocityComponent v = ComponentManager.Instance.GetEntityComponent<VelocityComponent>(Id);
            DirectionComponent dc = ComponentManager.Instance.GetEntityComponent<DirectionComponent>(Id);
            JumpComponent jump = ComponentManager.Instance.GetEntityComponent<JumpComponent>(Id);
            p.prevPosition = p.position;
            if (dc != null && v != null)
            {
                v.velocity.X = sideMovement * (int)dc.directio;
            }
            if (p != null && v != null && kbc != null && jump != null && dc != null)
            {
                if (kbc.state[ActionsEnum.Jump] == ButtonStates.Pressed && !pc.isFalling)
                {
                    if (dc.directio == Direction.Still)
                    {
                        dc.directio = dc.preDir;
                    }
                    if (v.velocity.Y > -jump.maxJumpHeight)
                    {
                        v.velocity.Y -= jump.jumpHeight;
                        ComponentManager.Instance.AddComponentToEntity(Id, new SoundEffectComponent("jump"));
                    }
                }
                v.velocity.Y += gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
                p.position += v.velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        private void GamePadeMove(int Id, GameTime gameTime)
        {
            GamePadComponent kbc = ComponentManager.Instance.GetEntityComponent<GamePadComponent>(Id);
            PositionComponent p = ComponentManager.Instance.GetEntityComponent<PositionComponent>(Id);
            PlayerComponent pc = ComponentManager.Instance.GetEntityComponent<PlayerComponent>(Id);
            VelocityComponent v = ComponentManager.Instance.GetEntityComponent<VelocityComponent>(Id);
            DirectionComponent dc = ComponentManager.Instance.GetEntityComponent<DirectionComponent>(Id);
            JumpComponent jump = ComponentManager.Instance.GetEntityComponent<JumpComponent>(Id);
            p.prevPosition = p.position;
            
            if (dc != null && v != null)
            {
                v.velocity.X = sideMovement * (int)dc.directio;
            }
            if (p != null && v != null && kbc != null && jump != null && dc != null)
            {
               // Console.WriteLine(kbc.gamepadStates[ActionsEnum.Jump]);
                if (kbc.gamepadStates[ActionsEnum.Jump] == ButtonStates.Pressed && !pc.isFalling)
                {
                    //Console.WriteLine(kbc.gamepadStates[ActionsEnum.Jump]);
                    if (dc.directio == Direction.Still)
                    {
                        dc.directio = dc.preDir;
                    }
                    if (v.velocity.Y > -jump.maxJumpHeight)
                    {
                        v.velocity.Y -= jump.jumpHeight;
                        ComponentManager.Instance.AddComponentToEntity(Id, new SoundEffectComponent("jump"));
                    }
                }
                v.velocity.Y += gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
                p.position += v.velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        /// <summary>
        /// flips the sprite depending on the direction of the player
        /// </summary>
        /// <param name="ID"> Id of the player </param>
        private void flip(int ID)
        {
            DirectionComponent dir = ComponentManager.Instance.GetEntityComponent<DirectionComponent>(ID);
            DrawableComponent draw = ComponentManager.Instance.GetEntityComponent<DrawableComponent>(ID);
            if (dir != null)
            {
                if (dir.directio == Direction.Right)
                {
                    draw.flip = SpriteEffects.None;
                }
                else if (dir.directio == Direction.Left)
                {
                    draw.flip = SpriteEffects.FlipHorizontally;
                }
            }
        }
    }
}
