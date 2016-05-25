using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Source.Systems.Interfaces
{
    /// <summary>
    /// An interface for obervers that act like systems but not are update, draw or input systems
    /// the interface purpose is to allow the "systems" to be stored in the manager
    /// ... rip english
    /// </summary>
    public interface IObserving :ISystem
    {
    }
}
