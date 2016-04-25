using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Source.Managers
{
    class SystemManager
    {
        private static SystemManager instance;

        // kanske
        // renderSystem Dictionary
        // updateSystem Dictionary


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



    }
}
