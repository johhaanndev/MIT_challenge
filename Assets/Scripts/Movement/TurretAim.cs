using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Control
{
    public class TurretAim : MonoBehaviour
    {
        [SerializeField] GameObject pivot;
        [SerializeField] GameObject turretBase;

        private List<GameObject> enemies = new List<GameObject>();
        private Transform target;

        void Update()
        {

        }

        public void Aim()
        {
            if (enemies.Count == 0)
            {
                return;
            }

            var closestDistance = 100f;
            target = GetClosestEnemy();

            RotateBase(target.position);
            RotatePivot(target);
        }

        private void RotateBase(Vector3 target)
        {
            var lookPos = target - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            turretBase.transform.rotation = rotation;
        }

        private void RotatePivot(Transform target)
        {
            pivot.transform.LookAt(target);
            Mathf.Clamp(pivot.transform.rotation.x, 0, 0.35f);
        }

        public Transform GetClosestEnemy()
        {
            var closestDistance = 100f;
            foreach (var enemy in enemies)
            {
                if ((enemy.transform.position - transform.position).magnitude < closestDistance)
                {
                    closestDistance = (enemy.transform.position - transform.position).magnitude;
                    target = enemy.transform;
                }
            }

            return target;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                enemies.Add(other.gameObject);
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                enemies.Remove(other.gameObject);
            }
        }

        public bool IsEnemyInRange()
        {
            return enemies.Count > 0;
        }
    }
}