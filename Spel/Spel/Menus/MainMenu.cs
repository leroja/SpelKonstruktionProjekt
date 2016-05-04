using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine;
using Microsoft.Xna.Framework.Input;
using GameEngine.Source.Enumerator;

namespace Spel.Menus
{
    public class MainMenu
    {
        private ECSGameEngine engine;

        public MainMenu(ECSGameEngine engine)
        {
            this.engine = engine;
        }
    }
}
