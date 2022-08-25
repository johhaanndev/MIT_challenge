using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Core
{
    public interface IGameCore
    {
        /// <summary>
        /// Initialize all components and references
        /// </summary>
        void Initialize();

        /// <summary>
        /// Set phase to end with winning message
        /// </summary>
        void WinGame();

        /// <summary>
        /// Set phase to end with losing message
        /// </summary>
        void LoseGame();
        
        /// <summary>
        /// Method to restart the gameplay scene
        /// </summary>
        void RestartGame();

    }
}
