using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine;

namespace Spel.Menus
{
    public class MenuHandler
    {
        private ECSGameEngine engine;

        public MenuHandler(ECSGameEngine engine)
        {
            this.engine = engine;
        }
    }
}
