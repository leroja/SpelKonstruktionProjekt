using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Source.Systems.Interfaces
{
    /// <summary>
    /// An interface for input systems
    /// </summary>
    public interface IInput : ISystem
    {
        void update(GameTime gameTime);
    }
}
