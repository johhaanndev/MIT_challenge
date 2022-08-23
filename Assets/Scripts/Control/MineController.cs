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
        private void ActivateMine()
        {
            fighter.Activation();
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                ActivateMine();
            }
        }
    }
}