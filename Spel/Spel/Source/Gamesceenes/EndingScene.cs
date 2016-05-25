using GameEngine.Source.Components;
using GameEngine.Source.Managers;
using GameEngine.Source.Systems.Interfaces;
using Spel.Source.Components;
using Spel.Source.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spel.Source.Gamestates
{
    /// <summary>
    /// EndingSceene is the class responsible for the ending state of the gameplay.
    /// </summary>
    class EndingScene : IGamescene
    {
        public List<int> entitiesInState { get; set; }

        /// <summary>
        /// endingScene constructor is responsible for managing the enteties which is used durring the 
        /// ending state of the gameplay, when someone is pronunced the winner
        /// </summary>
        public EndingScene()
        {
            /// some background, text and some presentation of the winner of the game.
                       
        }
        /// <summary>
        /// onSceneCreated this function is called whenever the current gamestate is changed. This function should contain logic that 
        /// needs to be processed before the state is shown for the player. This could be enteties that's not able to be created pre-runtime.
        /// </summary>
        public void onSceneCreated()
        {
            HealthSystem hs = (HealthSystem)SystemManager.Instance.RetrieveSystem<IUpdate>("HealthSystem");
            int id = hs.deathList.First();
            AIComponent ai = new AIComponent();
            ComponentManager.Instance.AddComponentToEntity(id, ai);
            ComponentManager.Instance.AddComponentToEntity(id, new SoundEffectComponent("winner"));
            PlayerComponent comp = ComponentManager.Instance.GetEntityComponent<PlayerComponent>(id);
            Console.Out.WriteLine(comp.playerName);
        }
        /// <summary>
        /// onSceneUpdate this function handles the logic for the state which should be run durring the update partion of the game.
        /// For example this could be to check for conditions to continue to the next state of the gameplay. 
        /// </summary>
        public void onSceneUpdate()
        {

        }
    }
}
