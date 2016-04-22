﻿using GameEngine.Source.Components;
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

        public Dictionary<Type, Dictionary<Entity, Component>> compDic = new Dictionary<Type, Dictionary<Entity, Component>>();

        public static EntityManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EntityManager();
                }
                return instance;
            }
        }

        public Entity CreateEntity()
        {
            Entity newEnity = new Entity();
            return newEnity;
        }

        public void addComponenet(Component component)
        {
            ComponentManager.Instance.AddComponent(newEntity, component);
        }
       
    }
}
