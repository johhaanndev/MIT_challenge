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
        [SerializeField] int layerIndexToIgnore;
        
        private PhaseChanger phaseChanger;

        private EnemyMover mover;
        private Health health;
        private EnemyFighter fighter;

        private List<GameObject> turrets = new List<GameObject>();
        private GameObject target;

        // Start is called before the first frame update
        void Start()
        {
            phaseChanger = GameObject.Find("PhaseChanger").GetComponent<PhaseChanger>();

            mover = GetComponent<EnemyMover>();
            health = GetComponent<Health>();
            fighter = GetComponent<EnemyFighter>();
            turrets = GameObject.FindGameObjectsWithTag("Player").ToList();

            Physics.IgnoreLayerCollision(layerIndexToIgnore, layerIndexToIgnore);
        }

        // Update is called once per frame
        void Update()
        {
            if (!phaseChanger.GetIsFight())
                return;

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
            if (target != null)
                mover.StartMoveAction(target.transform.position);
        }

        private GameObject GetClosestTurret(List<GameObject> turrets)
        {
            Debug.Log($"turrets: {turrets.Count}");
            turrets.RemoveAll(x => x.GetComponent<Health>().IsDead());

            if (turrets.Count == 0)
            {
                fighter.Cancel();
                return null;
            }

            target = GetTarget(turrets);

            return target;
        }

        private GameObject GetTarget(List<GameObject> turrets)
        {
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
            if (target == null)
                return false;

            float distanceToTarget = Vector3.Distance(target.transform.position, transform.position);
            return distanceToTarget <= attackRange;
        }

        public void DeleteTurretOnDestroy(GameObject turret)
        {
            turrets.Remove(turret);
        }

        public void AddTurretToList(GameObject turretPlaced)
        {
            Debug.Log("ADDED TURRET TO ENEMY LIST");
            turrets.Add(turretPlaced);
        }
    }
}