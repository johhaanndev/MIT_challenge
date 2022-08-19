using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float healthPoints = 100;

        private bool isDead = false;

        public bool IsDead() => isDead;

        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            if (healthPoints == 0)
                Die();
            Debug.Log($"{name} Taking {damage} points of damage. Current health: {healthPoints}");
        }

        private void Die()
        {
            if (isDead)
                return;

            isDead = true;
        }
    }
}