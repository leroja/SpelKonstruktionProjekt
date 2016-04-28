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
    public class SystemManager
    {
        private static SystemManager instance;

        public GameTime GameTime { get; set; }
        public SpriteBatch spriteBatch { get; set; }

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

        public void AddSystem(ISystem system)
        {
            Type type = system.GetType();
            if (system is IDraw)
            {
                AddSystemToList<IDraw>(renderSystems, system);
            }
            else if (system is IUpdate)
            {
                AddSystemToList<IUpdate>(updateSystems, system);
            }else if (system is IInput)
            {
                AddSystemToList<IInput>(inputSystems, system);
            }
        }

        public void RemoveSystem(ISystem system)
        {
            Type type = system.GetType();
            if (system is IDraw)
            {
                RemoveSystemFromList<IDraw>(renderSystems, system);
            }
            else if (system is IUpdate)
            {
                RemoveSystemFromList<IUpdate>(updateSystems, system);
            }
            else if (system is IInput)
            {
                AddSystemToList<IInput>(inputSystems, system);
            }

        }

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


        private void AddSystemToList<T>(List<T> list, ISystem system)
        {
            if (!list.Contains((T)system))
            {
                list.Add((T)system);
            }
        }

        private void RemoveSystemFromList<T>(List<T> list, ISystem system)
        {
            if (list != null)
            {
                if (list.Contains((T)system))
                {
                    list.Remove((T)system);
                }
            }
        }

    }
}
