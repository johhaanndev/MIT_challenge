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

        public void Activation()
        {
            StartCoroutine(Explode());
        }

        public IEnumerator Explode()
        {
            yield return new WaitForSeconds(countdown);
            Debug.Log("BOOM");
            var enemiesInRange = Physics.OverlapSphere(transform.position, explosionRange, enemiesLayer);

            foreach (var enemy in enemiesInRange)
            {
                enemy.GetComponent<Health>().TakeDamage(mineDamage);
            }
        }
    }
}