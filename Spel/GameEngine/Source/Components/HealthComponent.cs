using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Source.Components
{
    // Health Component
    public class HealthComponent : IComponent
    {
        // Max health
        public int health { get; set; }
        // Damage taken
        public int damage { get; set; }

        // Constructor for HealthComponent 
        public HealthComponent(int maxHealth)
        {
            health = maxHealth;
        }
    }
}
