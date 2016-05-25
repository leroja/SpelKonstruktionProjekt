using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Source.Systems.Interfaces
{
    /// <summary>
    /// An interface for updatingSystems
    /// </summary>
    public interface IUpdate : ISystem
    {
        void update(GameTime gameTime);
    }
}
