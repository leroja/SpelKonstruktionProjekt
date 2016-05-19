using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Source.Components
{
    public class FadeComponent : IComponent
    {
        public int alphaValue { get; set; }
        public int fadeIncrement { get; set; }
        public double fadeDelay = .035;

        public FadeComponent(int alpha, int fade)
        {
            alphaValue = alpha;
            fadeIncrement = fade;
        }
    }
}
