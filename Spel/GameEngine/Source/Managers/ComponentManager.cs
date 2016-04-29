using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Source.Components;

namespace GameEngine.Source.Managers
{
    public class ComponentManager
    {
        private static ComponentManager instance;

        private List<int> entityIDs;
        private int curMax;

        private Dictionary<Type, Dictionary<int, IComponent>> compDic = new Dictionary<Type, Dictionary<int, IComponent>>();

        private ComponentManager()
        {
            curMax = 10000;
            entityIDs = new List<int>();
            entityIDs.AddRange(Enumerable.Range(1, curMax));
        }


        public static ComponentManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ComponentManager();
                }
                return instance;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// 
        /// </returns>
        public int CreateID()
        {
            if (entityIDs.Count == 0) {
                entityIDs.AddRange(Enumerable.Range(curMax + 1, curMax + 10000));
                curMax += 10000;
            }

            //int id = entityIDs[0]; 
            int id = entityIDs[entityIDs.Count - 1];

            entityIDs.Remove(id);
            return id;
        }
        
        /// <summary>
        /// returns the id to the pool so it can be used again
        /// As a Game developer u also have to remove the the entity from the ComponentManager
        /// </summary>
        /// <param name="id"></param>
        public void RecycleID(int id)
        {
            entityIDs.Add(id);
        }

        /// <summary>
        /// "links" the component to the entity
        /// </summary>
        /// <param name="entity">
        /// 
        /// </param>
        /// <param name="component">
        /// 
        /// </param>
        public void AddComponentToEntity(int entityID, IComponent component)
        {
            Type type = component.GetType();

            if (compDic.ContainsKey(type))
            {
                compDic[type].Add(entityID, component);
            }
            else
            {
                compDic.Add(type, new Dictionary<int, IComponent>());
                compDic[type].Add(entityID, component);
            }

        }

        /// <summary>
        /// removes the component from the entity
        /// </summary>
        /// <param name="entity">
        /// the entity that has the component
        /// </param>
        /// <param name="component">
        /// the component to be removed
        /// </param>
        public void RemoveComponentFromEntity(int entityID, IComponent component)
        {

            Type type = component.GetType();

            if (compDic.ContainsKey(type))
            {
                if (compDic[type].ContainsKey(entityID))
                {
                    compDic[type].Remove(entityID);
                }
            }
        }

        /// <summary>
        /// returns a component if the Entity has one "linked" to it
        /// </summary>
        /// <typeparam name="T">
        /// the type of the requested component
        /// </typeparam>
        /// <param name="entity">
        /// 
        /// </param>
        /// <returns>
        /// a component of the requested type if there is one
        /// else it returns null
        /// </returns>
        public T GetEntityComponent<T>(int entityID) where T: IComponent
        {
            Type type = typeof(T);

            if (compDic.ContainsKey(type))
            {
                if (compDic[type].ContainsKey(entityID))
                {
                    return (T)compDic[type][entityID];
                }
            }
            return default(T);
        }

        /// <summary>
        /// returns an entity if there is an entity linked to the specific component
        /// </summary>
        /// <typeparam name="T">
        /// the type of compent
        /// </typeparam>
        /// <param name="component">
        /// the specific component
        /// </param>
        /// <returns>
        /// an entity if one is found
        /// else it returns null
        /// </returns>
        public int GetEntityIDByComponent<T>(IComponent component) where T : IComponent
        {
            Type type = typeof(T);

            if (compDic.ContainsKey(type)){
                foreach (KeyValuePair<int, IComponent> stuff in compDic[type])
                {
                    if (stuff.Value == component)
                    {
                        return stuff.Key;
                    }
                }
            }

            return 0;
        }
        
        /// <summary>
        /// return a list of all entites that "has" specific component
        /// or null if the component is not in the dictionary
        /// </summary>
        /// <typeparam name="T">
        /// 
        /// </typeparam>
        /// <returns>
        /// a list of all entities that "has" a specific component
        /// </returns>
        public List<int> GetAllEntitiesWithComponentType<T>() where T : IComponent
        {
            Type type = typeof(T);

            if (compDic.ContainsKey(type))
            {
                return compDic[type].Keys.ToList();
            }
            return null;
        }
        
        /// <summary>
        /// Removes an Entity from dictionary
        /// </summary>
        /// <param name="entity">
        /// the entity to be removed
        /// </param>

        public void RemoveEntity(int entityID)
        {
            foreach (KeyValuePair<Type, Dictionary<int, IComponent>> entry in compDic)
            {
                if (entry.Value.ContainsKey(entityID))
                    compDic[entry.Key].Remove(entityID);
            }
        }

    }
}
