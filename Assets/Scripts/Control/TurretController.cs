using Game.Core;
using Game.Fight;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Control
{
    public class TurretController : MonoBehaviour
    {
        private TurretFighter fighter;
        private TurretAim aim;
        private Health health;

        // Start is called before the first frame update
        void Start()
        {
            fighter = GetComponent<TurretFighter>();
            aim = GetComponent<TurretAim>();
            health = GetComponent<Health>();
        }

        // Update is called once per frame
        void Update()
        {
            if (health.IsDead())
                return;

            if (InAttackRangeOfEnemy())
            {
                InteractWithCombat();
            }

            InteractWithAim();
        }

        private void InteractWithCombat()
        {
            fighter.ShootBehaviour();
        }

        private bool InAttackRangeOfEnemy()
        {
            return aim.IsEnemyInRange();
        }



        private void InteractWithAim()
        {
            GetComponentInChildren<TurretAim>().Aim();
        }
    }
}
