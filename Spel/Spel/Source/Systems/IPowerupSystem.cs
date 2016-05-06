using GameEngine.Source.Systems.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spel.Source.Systems
{
    public interface IPowerupSystem : IUpdate
    {
        void OnPowerUpPicup(int id);
        void OnPowerUpColision();
        //string PowerUppDescription();
    }
}
