using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Source.Managers
{
    class RenderManager
    {

        private static RenderManager instance;


        private RenderManager()
        {

        }


        public static RenderManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RenderManager();
                }
                return instance;
            }
        }
    }
}
