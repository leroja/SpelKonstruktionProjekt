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


        public void AddComponent(Entity entity, Component component)
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


        public void RemoveComponentFRomEntity(Entity entity, Component component)
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

        public Entity GetEntityOfComponent<T>(Component component) where T : Component
        {
            Type type = typeof(T);

            if (compDic.ContainsKey(type)){

                if (compDic[type].ContainsValue(component))
                {
                    foreach (KeyValuePair<Entity, Component> stuff in compDic[type])
                    {
                        if (stuff.Value == component)
                        {
                            return stuff.Key;
                        }
                    }
                }
            }

            return null;
        }
        
        public List<Entity> GetAllEntitiesWithComponentType<T>() where T : Component
        {
            Type type = typeof(T);

            if (compDic.ContainsKey(type))
            {
                return compDic[type].Keys.ToList();
            }
            return null;
        }
        

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
