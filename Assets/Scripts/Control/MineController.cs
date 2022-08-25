using Game.Fight;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Control
{
    public class MineController : MonoBehaviour, IControllerBase
    {
        private MineFighter fighter;

        private void Start()
        {
            InitializeReferences();
        }

        public void InitializeReferences()
        {
            fighter = GetComponent<MineFighter>();
        }

        public void ActivateMine()
        {
            fighter.ExplodeMine();
        }
    }
}