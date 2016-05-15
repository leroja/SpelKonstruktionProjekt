using GameEngine.Source.Systems;
using GameEngine.Source.Systems.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Source.Managers
{
    /// <summary>
    /// A manager that stores the different systems in gameEngine and game
    /// </summary>
    public class SystemManager
    {
        private static SystemManager instance;

        public GameTime GameTime { get; set; }
        public SpriteBatch spriteBatch { get; set; }

        List<IObserving> observingSystems = new List<IObserving>();
        List<IDraw> renderSystems = new List<IDraw>();
        List<IUpdate> updateSystems = new List<IUpdate>();
        List<IInput> inputSystems = new List<IInput>();

        private SystemManager()
        {
        }

        public static SystemManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SystemManager();
                }
                return instance;
            }
        }

        /// <summary>
        /// Adds a system
        /// </summary>
        /// <param name="system"> The system that is going to be added </param>
        public void AddSystem(ISystem system)
        {
            Type type = system.GetType();
            if (system is IDraw)
            {
                AddSystemToList<IDraw>(renderSystems, system);
            }
            if (system is IUpdate)
            {
                AddSystemToList<IUpdate>(updateSystems, system);
            }
            if (system is IInput)
            {
                AddSystemToList<IInput>(inputSystems, system);
            }
            if(system is IObserving)
            {
                AddSystemToList<IObserving>(observingSystems, system);
            }
        }

        /// <summary>
        /// Removes a system 
        /// </summary>
        /// <param name="system"> The system that is to be removed </param>
        public void RemoveSystem(ISystem system)
        {
            Type type = system.GetType();
            if (system is IDraw)
            {
                RemoveSystemFromList<IDraw>(renderSystems, system);
            }
            if (system is IUpdate)
            {
                RemoveSystemFromList<IUpdate>(updateSystems, system);
            }
            if (system is IInput)
            {
                RemoveSystemFromList<IInput>(inputSystems, system);
            }
            if (system is IObserving)
            {
                RemoveSystemFromList<IObserving>(observingSystems, system);
            }
        }

        // @todo
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T RetrieveSystem<T>() where T : ISystem
        {
            throw new NotImplementedException();

        }

        /// <summary>
        /// Runs all updateable systems
        /// </summary>
        public void RunUpdateSystems()
        {
            if (updateSystems.Count > 0)
            {
                foreach (IUpdate system in updateSystems)
                {
                    system.update(GameTime);
                }
            }
        }
        
        /// <summary>
        /// Runs all input systems
        /// </summary>
        public void RunInputSystems()
        {
            if (updateSystems.Count > 0)
            {
                foreach (IInput system in inputSystems)
                {
                    system.update(GameTime);
                }
            }
        }

        /// <summary>
        /// Runs all rendering systems
        /// </summary>
        public void RunRenderSystems()
        {
            if (renderSystems.Count > 0)
            {
                foreach (IDraw system in renderSystems)
                {
                    system.draw(GameTime, spriteBatch);
                }
            }
        }


        /// <summary>
        /// a private method that adds the system to the correct list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="system"></param>
        private void AddSystemToList<T>(List<T> list, ISystem system)
        {
            if (!list.Contains((T)system))
            {
                list.Add((T)system);
            }
        }

        /// <summary>
        /// removes the system from the correct list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="system"></param>
        private void RemoveSystemFromList<T>(List<T> list, ISystem system)
        {
            if (list.Contains((T)system))
            {
                list.Remove((T)system);
            }
        }
    }
}
