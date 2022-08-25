using Game.Control;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Core
{
    public class Health : MonoBehaviour, IHealth
    {
        [SerializeField] float healthPoints = 100;
        [SerializeField] Image healthbar;
        [SerializeField] Image healthbarScreen;
        [SerializeField] ParticleSystem explosionParticles;

        private bool isDead = false;

        private GameCore gameCore;

        private float maxHealth;

        private void Start()
        {
            gameCore = GameObject.Find("GameCore").GetComponent<GameCore>();
            maxHealth = healthPoints;
        }

        public void Initialize()
        {
            gameCore = GameObject.Find("GameCore").GetComponent<GameCore>();
            maxHealth = healthPoints;
        }

        public void TakeDamage(float damage)
        {
            Debug.Log($"taking damage: {damage}");
            healthPoints = Mathf.Max(healthPoints - damage, 0);

            if (healthbar != null)
                FillHealthbar(healthPoints);

            if (healthPoints == 0)
                Die();
        }

        public void Die()
        {
            if (isDead)
                return;

            isDead = true;

            if (gameObject.name.Contains("Nexus"))
            {
                gameObject.GetComponent<NexusController>().NexusDestroyed(explosionParticles);
                return;
            }

            if (gameObject.name.Contains("Turret"))
            {
                var explosion = Instantiate(explosionParticles, transform.position, transform.rotation, null);
                gameObject.SetActive(false);
                return;
            }

            if (gameObject.GetComponent<Animator>() != null)
            {
                gameCore.RemoveDeadEnemy(gameObject);

                GetComponent<Animator>().SetTrigger("die");
                GetComponent<ActionScheduler>().CancelCurrentAction();

                GetComponent<Rigidbody>().useGravity = false;
                GetComponent<CapsuleCollider>().enabled = false;
            }
        }

        public bool IsDead() => isDead;

        private void FillHealthbar(float currentHealth)
        {
            healthbar.fillAmount = currentHealth / maxHealth;

            if (gameObject.name.Equals("Nexus"))
                healthbarScreen.fillAmount = currentHealth / maxHealth;
        }
    }
}
