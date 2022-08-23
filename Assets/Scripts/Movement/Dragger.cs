using Game.Control;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Movement
{

    public class Dragger : MonoBehaviour
    {
        private Vector3 positionToInstantiate;

        [SerializeField] List<GameObject> enemies;

        private GameObject lastTurretPlaced;
        private List<GameObject> turretsPlaced = new List<GameObject>();

        public void Drag(GameObject turret)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                turret.SetActive(true);
                Debug.DrawLine(ray.origin, hit.point, Color.red);
                turret.transform.position = hit.point;

                positionToInstantiate = turret.transform.position;
            }
            else
            {
                turret.SetActive(false);
            }
        }

        public void Drop(GameObject turretPlanning, GameObject turretPrefab)
        {
            turretPlanning.SetActive(false);
            if (!CanPlaceTurret(turretPlanning))
                return;

            GameObject turretToInstantiate = Instantiate(turretPrefab, positionToInstantiate, Quaternion.identity, GameObject.Find("Turrets").transform);
            turretToInstantiate.SetActive(true);
            
            AddTurretToEnemiesList(turretToInstantiate);
            turretsPlaced.Add(turretToInstantiate);

            if (turretsPlaced.Count > 0)
                lastTurretPlaced = turretsPlaced[turretsPlaced.Count - 1];
        }

        private void AddTurretToEnemiesList(GameObject turret)
        {
            foreach (var enemy in enemies)
                enemy.GetComponent<AIController>().AddTurretToList(turret);
        }

        private bool CanPlaceTurret(GameObject turretPlanning)
        {
            var colliders = Physics.OverlapSphere(positionToInstantiate, 2.5f);
            foreach (var collider in colliders)
            {
                if (collider.CompareTag("Player"))
                {
                    var distance = Vector3.Distance(collider.transform.position, turretPlanning.transform.position);
                    if (distance <= 2.5f)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public void UndoLastTurret()
        {
            if (lastTurretPlaced != null)
            {
                turretsPlaced.Remove(turretsPlaced[turretsPlaced.Count - 1]);
                Destroy(lastTurretPlaced.gameObject);

                if (turretsPlaced.Count > 0)
                    lastTurretPlaced = (turretsPlaced[turretsPlaced.Count - 1]);
            }
            else
                Debug.Log("No turrets placed");
        }
    }

}