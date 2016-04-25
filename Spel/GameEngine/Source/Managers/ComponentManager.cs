using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Source.Components;
using GameEngine.Source.EntityStuff;

namespace GameEngine.Source.Managers
{
    class ComponentManager
    {
        private static ComponentManager instance;


        public Dictionary<Type, Dictionary<Entity, Component>> compDic = new Dictionary<Type, Dictionary<Entity, Component>>();

        private ComponentManager()
        {

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
        /// "links" the component to the entity
        /// </summary>
        /// <param name="entity">
        /// 
        /// </param>
        /// <param name="component">
        /// 
        /// </param>
        public void AddComponentToEntity(Entity entity, Component component)
        {
            Type type = component.GetType();

            if (compDic.ContainsKey(type))
            {
                compDic[type].Add(entity, component);
            }
            else
            {
                compDic.Add(type, new Dictionary<Entity, Component>());
                compDic[type].Add(entity, component);
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
        public void RemoveComponentFromEntity(Entity entity, Component component)
        {

            Type type = component.GetType();

            if (compDic.ContainsKey(type))
            {
                if (compDic[type].ContainsKey(entity))
                {
                    compDic[type].Remove(entity);
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
        public T GetEntityComponent<T>(Entity entity) where T: Component
        {
            Type type = typeof(T);

            if (compDic.ContainsKey(type))
            {
                if (compDic[type].ContainsKey(entity))
                {
                    return (T)compDic[type][entity];
                }
            }
            
            return null;
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
        public Entity GetEntityByComponent<T>(Component component) where T : Component
        {
            Type type = typeof(T);

            if (compDic.ContainsKey(type)){
                foreach (KeyValuePair<Entity, Component> stuff in compDic[type])
                {
                    if (stuff.Value == component)
                    {
                        return stuff.Key;
                    }
                }
            }

            return null;
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
        public List<Entity> GetAllEntitiesWithComponentType<T>() where T : Component
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

        public void RemoveEntity(Entity entity)
        {
            foreach (KeyValuePair<Type, Dictionary<Entity, Component>> entry in compDic)
            {
                if (entry.Value.ContainsKey(entity))
                    compDic[entry.Key].Remove(entity);
            }
        }

    }
}
