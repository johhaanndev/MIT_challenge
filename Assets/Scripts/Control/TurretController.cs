using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Control
{
    public class TurretController : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            InteractWithAim();
        }

        private void InteractWithAim()
        {
            GetComponentInChildren<TurretAim>().Aim();
        }
    }
}
