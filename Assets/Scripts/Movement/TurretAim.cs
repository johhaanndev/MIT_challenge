using Game.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Movement
{
    public class TurretAim : MonoBehaviour
    {
        [SerializeField] GameObject pivot;
        [SerializeField] GameObject turretBase;

        private List<GameObject> enemies = new List<GameObject>();
        private Transform target;

        public void Aim()
        {
            if (enemies.Count == 0)
            {
                return;
            }

            var closestDistance = 100f;

            enemies.RemoveAll(x => x.GetComponent<Health>().IsDead());

            target = GetClosestEnemy();
            
            if (target != null)
            {
                RotateBase(target.position);
                RotatePivot(target);
            }
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
                    if (!enemy.GetComponent<Health>().IsDead())
                        target = enemy.transform;
                }
            }

            return target;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(GameTags.ENEMY))
            {
                enemies.Add(other.gameObject);
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(GameTags.ENEMY))
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