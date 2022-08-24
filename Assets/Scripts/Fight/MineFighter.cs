using Game.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Fight
{
    public class MineFighter : MonoBehaviour
    {
        [SerializeField] float countdown = 0;
        [SerializeField] float explosionRange = 2;
        [SerializeField] float mineDamage = 30;
        [SerializeField] LayerMask enemiesLayer;
        [SerializeField] ParticleSystem explosionParticles;

        public void ExplodeMine()
        {
            var explosion = Instantiate(explosionParticles, transform.position, transform.rotation);

            var enemiesInRange = Physics.OverlapSphere(transform.position, explosionRange, enemiesLayer);

            foreach (var enemy in enemiesInRange)
            {
                enemy.GetComponent<Health>().TakeDamage(mineDamage);
            }

            Destroy(gameObject);
        }
    }
}