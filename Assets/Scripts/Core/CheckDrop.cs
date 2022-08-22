using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core
{

    public class CheckDrop : MonoBehaviour
    {
        private bool canPlaceTurret = true;

        public void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Can't place it here");
            }
        }
    }

}