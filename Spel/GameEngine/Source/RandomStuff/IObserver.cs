﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Source.RandomStuff
{
    /// <summary>
    /// 
    /// </summary>
    public interface IObserver
    {
        void update(IEvent t);
    }
}
