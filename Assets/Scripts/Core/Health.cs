using Game.Control;
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

        private GameCore gameCore;

        private void Start()
        {
            gameCore = GameObject.Find("GameCore").GetComponent<GameCore>();
        }

        public bool IsDead() => isDead;

        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            if (healthPoints == 0)
                Die();
        }

        private void Die()
        {
            if (isDead)
                return;

            isDead = true;

            if (gameObject.name.Contains("Nexus"))
            {
                gameObject.GetComponent<NexusController>().NexusDestroyed();
                return;
            }

            if (gameObject.name.Contains("Turret"))
            {
                return;
            }

            if (gameObject.GetComponent<Animator>() != null)
            {
                Debug.Log("Enemy killed");
                gameCore.RemoveDeadEnemy(gameObject);

                GetComponent<Animator>().SetTrigger("die");
                GetComponent<ActionScheduler>().CancelCurrentAction();

                GetComponent<Rigidbody>().useGravity = false;
                GetComponent<CapsuleCollider>().enabled = false;
            }
        }
    }
}