using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameEngine.Source.Managers;
using GameEngine.Source.Components;
using GameEngine.Source.Systems.Interfaces;

namespace GameEngine.Source.Systems
{
    /// <summary>
    /// TextRenderSystem the class responsible for handling the drawing of text sprites
    /// </summary>
    class TextRenderSystem : IDraw
    {
        /// <summary>
        /// draw is the function which handles the actuall drawing of the text in each loop in the game.
        /// </summary>
        /// <param name="gameTime">Takes a GameTime object which should represent the time since the last time this function was called</param>
        /// <param name="spriteBatch">Takes a SpriteBatch object which is used to draw the textures directly to the screen</param>
        public void draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            List<int> dra = ComponentManager.Instance.GetAllEntitiesWithComponentType<DrawableTextComponent>();
            if (dra != null)
            {
                foreach (var a in dra)
                {
                    
                    DrawableTextComponent d = ComponentManager.Instance.GetEntityComponent<DrawableTextComponent>(a);
                    FadeComponent f = ComponentManager.Instance.GetEntityComponent<FadeComponent>(a);
                    if(f != null)
                        f.fadeDelay -= gameTime.ElapsedGameTime.TotalSeconds;

                    Dictionary<Vector2, string> list = d.getMenuList();
                    //If there is a menulist then there's multiple lines of text that should be written to the screen. 
                    if(list != null)
                    {
                        //For each of the strings in the list write them to the screen, the value key in the dictionary is the
                        //position where the line should be written.
                        foreach(var item in list)
                        {
                            if(f != null)
                            {
                                if (f.fadeDelay <= 0)
                                {
                                    f.fadeDelay = .035;

                                    f.alphaValue += f.fadeIncrement;

                                    if (f.fadeIncrement >= 255 || f.alphaValue <= 0)
                                        f.fadeIncrement *= -1;
                                }
                            }
                            d.textColor = new Color(255, 255, 255, (byte)MathHelper.Clamp(f.alphaValue,0 , 255));
                            spriteBatch.DrawString(d.font, item.Value, item.Key, d.textColor);
                        }
                    }
                    //There is just one line of text, then just draw it to the screen.
                    else
                    {
                        PositionComponent p = ComponentManager.Instance.GetEntityComponent<PositionComponent>(a);
                        if (p != null && d != null && d.visable == true)
                        {
                            spriteBatch.DrawString(d.font, d.text, p.position, d.textColor);
                        }
                    }
                }
            }
        }
    }
}
