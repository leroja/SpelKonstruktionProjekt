using GameEngine.Source.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spel.Source.Components
{
    // Health Component
    public class HealthComponent : IComponent
    {
        public int maxhealth { get; set; }
        // Health
        public int health { get; set; }

        // Constructor for HealthComponent 
        public HealthComponent(int maxHealth)
        {
            this.maxhealth = maxHealth;
            health = maxHealth;
        }
    }
}
