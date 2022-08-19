using Game.Control;
using Game.Core;
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

            if (timeSinceLastAttack >= timeBetweenAttacks)
            {
                target = enemy.gameObject;

                timeSinceLastAttack = 0;
                var direction = target.transform.position - shootingSpot.position;
                var bullet = Instantiate(projectile, shootingSpot.transform.position, shootingSpot.transform.rotation, null);
                bullet.GetComponent<Rigidbody>().velocity = direction.normalized * ShootForce;
            }
        }

    }
}
