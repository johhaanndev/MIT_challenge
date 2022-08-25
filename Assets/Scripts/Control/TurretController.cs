using Game.Core;
using Game.Fight;
using Game.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Control
{
    public class TurretController : MonoBehaviour, IControllerBase
    {
        private TurretFighter fighter;
        private TurretAim aim;
        private Health health;

        private PhaseChanger phaseChanger;

        // Start is called before the first frame update
        void Start()
        {
            InitializeReferences();
        }

        public void InitializeReferences()
        {
            phaseChanger = GameObject.Find("PhaseChanger").GetComponent<PhaseChanger>();
            phaseChanger.AddTurretToList(gameObject);

            fighter = GetComponent<TurretFighter>();
            aim = GetComponent<TurretAim>();
            health = GetComponent<Health>();
        }

        // Update is called once per frame
        void Update()
        {
            if (!phaseChanger.GetIsFight())
                return;

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
