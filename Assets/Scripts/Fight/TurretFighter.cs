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
        [SerializeField] float timeBetweenAttacks;
        [SerializeField] float turretDamage;
        [SerializeField] LayerMask mask;
        [SerializeField] TrailRenderer bulletTrail;

        [SerializeField] List<GameObject> muzzles;

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
            if (GetComponent<Health>().IsDead())
                return;

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

                    ShootRayToEnemy(direction);
                }
            }
        }

        private void ShootRayToEnemy(Vector3 dir)
        {
            var index = UnityEngine.Random.Range(0, muzzles.Count - 1);
            var muzz = Instantiate(muzzles[index], shootingSpot.transform.position, shootingSpot.transform.rotation, shootingSpot.transform);
            StartCoroutine(DestroyMuzzle(muzz));

            RaycastHit hit;
            if (Physics.Raycast(shootingSpot.position, dir, out hit, float.MaxValue, ~mask))
            {
                if (!hit.transform.name.Contains("enemy"))
                    return;

                if (hit.transform.gameObject != null)
                {
                    TrailRenderer hotTrail = Instantiate(bulletTrail, shootingSpot.position, Quaternion.identity);
                    StartCoroutine(SpawnTrail(hotTrail, hit));
                    hit.transform.GetComponent<Health>().TakeDamage(turretDamage);
                }
            }
        }

        private IEnumerator DestroyMuzzle(GameObject muzzle)
        {
            yield return new WaitForSeconds(0.05f);
            Destroy(muzzle);
        }

        private IEnumerator SpawnTrail(TrailRenderer trail, RaycastHit hit)
        {
            float time = 0;
            Vector3 startPosition = trail.transform.position;
            while (time < 1)
            {
                trail.transform.position = Vector3.Lerp(startPosition, hit.point, time);
                time += Time.deltaTime / trail.time;

                yield return null;
            }
            trail.transform.position = hit.point;

            Destroy(trail.gameObject, trail.time);
        }

        void OnDrawGizmos()
        {
            if (target == null || GetComponent<Health>().IsDead())
                return;

            Gizmos.color = Color.green;
            Vector3 direction = target.transform.position - shootingSpot.transform.position;
            Gizmos.DrawRay(shootingSpot.transform.position, direction);
        }
    }
}
