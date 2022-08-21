using Game.Core;
using Game.Fight;
using Game.Movement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float attackRange;

        private EnemyMover mover;
        private Health health;
        private EnemyFighter fighter;

        private List<GameObject> turrets = new List<GameObject>();
        private GameObject target;

        // Start is called before the first frame update
        void Start()
        {
            mover = GetComponent<EnemyMover>();
            health = GetComponent<Health>();
            fighter = GetComponent<EnemyFighter>();
            turrets = GameObject.FindGameObjectsWithTag("Player").ToList();
        }

        // Update is called once per frame
        void Update()
        {
            if (health.IsDead())
                return;

            if (turrets.Count == 0)
                return;

            if (!InAttackRangeOfTarget())
            {
                PursueBehaviour();
                return;
            }

            AttackBehaviour();
        }

        private void AttackBehaviour()
        {
            fighter.AttackAction(target);
        }

        private void PursueBehaviour()
        {
            mover.StartMoveAction(target.transform.position);
        }

        private GameObject GetClosestTurret(List<GameObject> turrets)
        {
            if (turrets.Count == 0)
            {
                fighter.Cancel();
                return null;
            }

            float minDistance = Mathf.Infinity;
            foreach (var turret in turrets)
            {
                if (Vector3.Distance(turret.transform.position, transform.position) < minDistance)
                {
                    minDistance = Vector3.Distance(turret.transform.position, transform.position);
                    target = turret;
                }
            }

            return target;
        }

        private bool InAttackRangeOfTarget()
        {
            target = GetClosestTurret(turrets);
            float distanceToTarget = Vector3.Distance(target.transform.position, transform.position);
            return distanceToTarget <= attackRange;
        }

        public void DeleteTurretOnDestroy(GameObject turret)
        {
            turrets.Remove(turret);
        }
    }
}