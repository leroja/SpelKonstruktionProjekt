using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spel.Source.Enum
{
    /// <summary>
    /// An enum for defining different types of collisions
    /// </summary>
    public enum CollisionTypes
    {
        PlayerVsPlayer, PlayerVsWall, PlayerVsPlatform, PlayerVsPowerup, PlayerVsCube, NotDefined
    }
}
