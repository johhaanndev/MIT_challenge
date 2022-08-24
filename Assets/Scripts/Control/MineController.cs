using Game.Fight;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Control
{
    public class MineController : MonoBehaviour
    {
        private MineFighter fighter;

        private void Start()
        {
            fighter = GetComponent<MineFighter>();
        }

        public void ActivateMine()
        {
            fighter.ExplodeMine();
        }
    }
}