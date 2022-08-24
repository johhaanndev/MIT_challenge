using Game.Fight;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Control
{
    public class MineController : MonoBehaviour
    {
        [SerializeField] int price = 5;

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