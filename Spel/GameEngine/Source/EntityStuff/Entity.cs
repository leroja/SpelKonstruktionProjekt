using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Source.EntityStuff
{
    /// <summary>
    /// The Entity class is the objects which the components will be applied to. 
    /// </summary>
    public class Entity
    {
        public int entityID { get; set; }
        /// <summary>
        /// Default constructor for the Entity class.
        /// </summary>
        public Entity(int id) {
            entityID = id;
        }
    }
}
