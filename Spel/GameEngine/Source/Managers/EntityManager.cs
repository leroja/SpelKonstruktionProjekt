using GameEngine.Source.Components;
using GameEngine.Source.EntityStuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Source.Managers
{
    /// <summary>
    /// EntityManager class, to manage the creation and number of enteties.
    /// Entitymanager is a singleton.
    /// </summary>
    public class EntityManager
    {
        private static EntityManager instance;
        private int currEnteties;
        

        public static EntityManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EntityManager();
                    instance.currEnteties = 0;
                }
                return instance;
            }
        }

        /// <summary>
        /// CreateEntity method handles the creation of the enteties. 
        /// </summary>
        /// <returns></returns>
        public Entity CreateEntity()
        {
            currEnteties++;
            Entity newEnity = new Entity(currEnteties);
            return newEnity;
        }

        /// <summary>
        /// getNumberOfEnteties is used to get information regarding the number of enteties currently available.
        /// </summary>
        /// <returns>returns an int which corresponds to the number of enteties created.</returns>
        public int getNumberOfEnteties()
        {
            return currEnteties;
        }

    }
}
