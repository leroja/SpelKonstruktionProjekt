﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Source.Components;

namespace GameEngine.Source.Managers
{
    /// <summary>
    /// handles storage of entities and their components
    /// also handles the creation of new entites and removal of them
    /// </summary>
    public class ComponentManager
    {
        private static ComponentManager instance;

        private List<int> entityIDs;
        private int curMax;
        private const int step = 10000;

        private Dictionary<Type, Dictionary<int, IComponent>> compDic = new Dictionary<Type, Dictionary<int, IComponent>>();

        private ComponentManager()
        {
            curMax = step;
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

        public int CopyEnitity(List<IComponent> list)
        {
            int id = CreateID();

            foreach (var item in list)
            {
                AddComponentToEntity(id, item);
            }
            return id;
        }

        public void Clear()
        {
            foreach (var a in entityIDs)
            {
                RemoveEntity(a);
                RecycleID(a);
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
                entityIDs.AddRange(Enumerable.Range(curMax + 1, curMax + step));
                curMax += step;
            }
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
            if(!entityIDs.Contains(id))
                entityIDs.Add(id);
        }

        /// <summary>
        /// "links" the component to the entity
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="component"></param>
        public void AddComponentToEntity(int entityID, IComponent component)
        {
            Type type = component.GetType();
            if (compDic.ContainsKey(type))
            {
                try
                {
                    compDic[type].Add(entityID, component);
                }
                catch (Exception)
                {

                }
                
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
        /// <param name="entity"> The entity that has the component </param>
        /// <param name="component"> The component to be removed </param>
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
        /// The type of the requested component
        /// </typeparam>
        /// <param name="entity"></param>
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
        /// <typeparam name="T"> The type of compent </typeparam>
        /// <param name="component"> The specific component </param>
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
        /// <typeparam name="T"></typeparam>
        /// <returns>
        /// A list of all entities that "has" a specific component
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
        /// as a game Dev you also have to recycle the id
        /// </summary>
        /// <param name="entity"> The entity to be removed </param>
        public void RemoveEntity(int entityID)
        {
            foreach (KeyValuePair<Type, Dictionary<int, IComponent>> entry in compDic)
            {
                if (entry.Value.ContainsKey(entityID))
                    compDic[entry.Key].Remove(entityID);
            }
        }

        /// <summary>
        /// Returns a list of all components that belong to an entity
        /// </summary>
        /// <param name="Id"> The Id of the entity  </param>
        /// <returns>
        /// A list of the entity's all components
        /// </returns>
        public List<IComponent> GetAllEntityComponents(int Id)
        {
            List<IComponent> compList = new List<IComponent>();

            foreach (KeyValuePair<Type, Dictionary<int, IComponent>> entry in compDic)
            {
                if (entry.Value.ContainsKey(Id))
                {
                    compList.Add(entry.Value[Id]);
                }
            }
            return compList;
        }

        /// <summary>
        /// return a dictionary with all entites that "has" specific type of component & 
        /// and the components of that type
        /// </summary>
        /// <typeparam name="T"> The type of component </typeparam>
        /// <returns>
        /// A dictionary containing id's and components of the specified type
        /// </returns>
        public Dictionary<int, IComponent> GetAllEntitiesAndComponentsWithComponentType<T>() where T : IComponent
        {
            Type type = typeof(T);

            if (compDic.ContainsKey(type))
            {
                return compDic[type]; ;
            }
            return null;
        }

        /// <summary>
        /// Checks whether an entity has a particular type of component
        /// </summary>
        /// <typeparam name="T"> The type of component </typeparam>
        /// <param name="ent"> The id of the enetity </param>
        /// <returns>
        /// true if the entity has a component of that type else false
        /// </returns>
        public bool CheckIfEntityHasComponent<T>(int ent) where T : IComponent
        {
            Type type = typeof(T);

            if (compDic.ContainsKey(type))
            {
                if (compDic[type].ContainsKey(ent))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
