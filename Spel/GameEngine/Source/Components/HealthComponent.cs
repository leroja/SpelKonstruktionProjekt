using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Source.Components
{
    // Health Component
    public class HealthComponent : Component
    {
        // Max health
        int health { get; set; }
        // Damage taken
        int damage { get; set; }

        // Constructor for HealthComponent 
        public HealthComponent(int maxHealth)
        {
            health = maxHealth;
        }
    }
}
