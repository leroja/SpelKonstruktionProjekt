using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Source.Systems.Interfaces
{
    public interface IPowerupSystem : IUpdate
    {
        void OnPowerUpPicup(int id);
        void OnPowerUpColision();
        //string PowerUppDescription();
    }
}
