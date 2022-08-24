using Game.Control;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Fight
{
    public class MineActivator : MonoBehaviour
    {
        public void RedButtonPressed()
        {
            var mines = GameObject.FindGameObjectsWithTag("Mine");
            foreach (var mine in mines)
            {
                mine.GetComponent<MineController>().ActivateMine();
            }
        }
    }
}