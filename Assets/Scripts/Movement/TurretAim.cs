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
            Debug.Log($"Enemies in range: {enemies.Count}");
        }

        public void Aim()
        {
            if (enemies.Count == 0)
            {
                return;
            }

            var closestDistance = 50f;
            foreach(var enemy in enemies)
            {
                if ((enemy.transform.position - transform.position).magnitude < closestDistance)
                {
                    closestDistance = (enemy.transform.position - transform.position).magnitude;
                    target = enemy.transform;
                }
            }

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
    }
}