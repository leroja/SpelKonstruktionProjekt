using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Source.Components
{
    public class TTLComponent: IComponent
    {
        public float curTime { get; set; }
        public float maxTime { get; set; }



        public TTLComponent(float max)
        {
            maxTime = max;
        }
    }
}
