using GameEngine.Source.Components;
using GameEngine.Source.EntityStuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Source.Managers
{
    public class EntityManager
    {
        private static EntityManager instance;
        private int currEnteties;

        public Dictionary<Type, Dictionary<Entity, Component>> compDic = new Dictionary<Type, Dictionary<Entity, Component>>();

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

        public Entity CreateEntity()
        {
            currEnteties++;
            Entity newEnity = new Entity(currEnteties);
            return newEnity;
        }

        public int getNumberOfEnteties()
        {
            return currEnteties;
        }

    }
}
