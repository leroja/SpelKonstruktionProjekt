using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Source.Entity
{
    /// <summary>
    /// The Entity class is the objects which the components will be applied to. 
    /// </summary>
    class Entity
    {
        public bool visable { get; set; }
        public bool updateable { get; set; }
        /// <summary>
        /// Default constructor for the Entity class.
        /// </summary>
        Entity() {
            visable = true;
            updateable = true;
        }
    }
}
