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

        private Health target;
        private float timeSinceLastAttack = Mathf.Infinity;

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null)
                return;

            if (target.IsDead())
                return;

            if (target == null)
                return;

            Debug.Log($"Is in range? {GetIsInRange()}");
            if (!GetIsInRange())
            {
                GetComponent<EnemyMover>().MoveTo(target.transform.position);
            }
            else
            {
                GetComponent<EnemyMover>().Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform.position);

            if (timeSinceLastAttack >= timeBetweenAttacks)
            {
                timeSinceLastAttack = 0;
                TriggerAttack();
            }

        }

        private void TriggerAttack()
        {
            //GetComponent<Animator>().ResetTrigger("stopAttack");
            //GetComponent<Animator>().SetTrigger("attack");
        }

        /// <summary>
        /// Animation Even when attacking
        /// </summary>
        private void Hit()
        {
            if (target == null)
                return;

            Debug.Log("Hit!");
            //target.TakeDamage(weaponDamage);
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
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
            target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            StopAttack();
            target = null;
            GetComponent<EnemyMover>().Cancel();
        }

        private void StopAttack()
        {
            //GetComponent<Animator>().ResetTrigger("attack");
            //GetComponent<Animator>().SetTrigger("stopAttack");
        }

        public void SetTarget(GameObject target) => this.target = target.GetComponent<Health>();
    }
}