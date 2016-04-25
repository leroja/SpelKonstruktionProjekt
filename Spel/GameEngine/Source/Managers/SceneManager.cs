using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Source.Managers
{
    class SceneManager
    {
        private static SceneManager instance;


        private SceneManager()
        {

        }


        public static SceneManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SceneManager();
                }
                return instance;
            }
        }




    }
}
