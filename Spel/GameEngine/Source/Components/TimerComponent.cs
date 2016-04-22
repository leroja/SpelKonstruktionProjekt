﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace GameEngine.Source.Components
{
    /// <summary>
    /// This class is used to make sure certain events only last for a given amount of time.
    /// </summary>
    class TimerComponent : Component
    {
        /// <summary>
        /// A timer to keep track of the elapsed time. The timer creates an event when the given time has 
        /// elapsed.
        /// </summary>
        private Timer timer;
        /// <summary>
        /// The constructor which initalizes the timer with the time the entity should last for.
        /// </summary>
        /// <param name="time"></param>
        TimerComponent(double time)
        {
            timer = new Timer(time);
        }
    }
}
