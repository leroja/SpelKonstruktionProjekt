using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Source.EntityStuff
{
    /// <summary>
    /// The Entity class is the objects which the components will be applied to. 
    /// The entities contains a ID which is uniqe for each entity in order to find 
    /// specific enteties. Therefore the id when constructing new entities should be uniqe.
    /// </summary>
    class Entity
    {
        int entityID { get; set; }
        /// <summary>
        /// Default constructor for the Entity class.
        /// </summary>
        /// <param name="id">Takes a ID which is uniqe for each entity in order to find 
        /// specific enteties. Therefore the id when constructing new entities should be uniqe.</param>
        Entity(int id) {
            entityID = id;
        }
    }
}
