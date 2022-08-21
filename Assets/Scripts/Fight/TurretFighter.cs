using Game.Control;
using Game.Core;
using Game.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Fight
{

    public class TurretFighter : MonoBehaviour
    {
        [SerializeField] Transform shootingSpot;
        [SerializeField] GameObject projectile;
        [SerializeField] float timeBetweenAttacks;
        [SerializeField] float ShootForce;
        [SerializeField] float turretDamage;
        [SerializeField] LayerMask mask;

        private TurretAim aim;

        private GameObject target;

        private float timeSinceLastAttack = 0;

        // Start is called before the first frame update
        void Start()
        {
            aim = GetComponent<TurretAim>();
        }

        // Update is called once per frame
        void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
        }


        public void ShootBehaviour()
        {
            var enemy = aim.GetClosestEnemy();
            if (enemy != null)
            {
                if (timeSinceLastAttack >= timeBetweenAttacks)
                {
                    timeSinceLastAttack = 0;
                    target = enemy.gameObject;
                    var direction = target.transform.position - shootingSpot.position;

                    CastRayToEnemy(direction);
                    InstantiateBullets(direction);
                }
            }
        }

        private void CastRayToEnemy(Vector3 dir)
        {
            RaycastHit hit;
            if (Physics.Raycast(shootingSpot.position, dir, out hit, float.MaxValue, ~mask))
            {
                Debug.Log($"hit from {gameObject.name} to {hit.transform.name}");
                if (hit.transform.gameObject != null)
                {
                    hit.transform.GetComponent<Health>().TakeDamage(turretDamage);
                }
            }
        }

        private void InstantiateBullets(Vector3 dir)
        {
            var bullet = Instantiate(projectile, shootingSpot.transform.position, shootingSpot.transform.rotation, null);
            bullet.GetComponent<Rigidbody>().velocity = dir.normalized * ShootForce;
        }
    }
}
