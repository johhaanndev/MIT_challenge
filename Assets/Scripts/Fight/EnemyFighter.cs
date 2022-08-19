using Game.Core;
using Game.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Fight
{
    public class EnemyFighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2.0f;
        [SerializeField] float timeBetweenAttacks = 1.0f;
        [SerializeField] float weaponDamage = 5.0f;
        [SerializeField] GameObject firingSpot;

        private Health turretTarget;
        private float timeSinceLastAttack = Mathf.Infinity;

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (turretTarget == null)
                return;

            if (turretTarget.IsDead())
                return;

            if (turretTarget == null)
                return;

            if (!GetIsInRange())
            {
                GetComponent<EnemyMover>().MoveTo(turretTarget.transform.position);
            }
            else
            {
                GetComponent<EnemyMover>().Cancel();
                StartAttack();
            }
        }

        private void StartAttack()
        { 
            transform.LookAt(turretTarget.transform.position);

            if (timeSinceLastAttack >= timeBetweenAttacks)
            {
                timeSinceLastAttack = 0;
                Shoot(firingSpot.transform.position, turretTarget.transform.position);
                TriggerAttack();
            }

        }

        private void Shoot(Vector3 from, Vector3 targetPosition)
        {
            float maxRange = weaponRange + 2; // + 2 just to be sure
            RaycastHit hit;
            if (Vector3.Distance(from, targetPosition) < maxRange)
            {
                if (Physics.Raycast(from, (targetPosition - from), out hit, maxRange))
                {
                    if (hit.transform.CompareTag("Player"))
                    {
                        Hit(hit.transform.GetComponent<Health>());
                    }
                }
            }
        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("attack");
        }

        /// <summary>
        /// Animation Even when attacking
        /// </summary>
        private void Hit(Health target)
        {
            if (target == null)
                return;

            target.TakeDamage(weaponDamage);
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, turretTarget.transform.position) < weaponRange;
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null)
                return false;

            Health targetToCheck = combatTarget.GetComponent<Health>();
            return targetToCheck != null && !targetToCheck.IsDead();
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            turretTarget = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            StopAttack();
            turretTarget = null;
            GetComponent<EnemyMover>().Cancel();
        }

        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }

        public void SetTarget(GameObject target) => this.turretTarget = target.GetComponent<Health>();
    }
}