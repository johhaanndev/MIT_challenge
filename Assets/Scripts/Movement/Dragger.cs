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

        private GameObject lastObjectPlaced;
        private List<GameObject> objectsPlaced = new List<GameObject>();

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

        public void Drop(GameObject turretPlanning, GameObject prefab)
        {
            turretPlanning.SetActive(false);
            if (!CanPlaceTurret(turretPlanning))
                return;

            GameObject prefabToInstantiate = Instantiate(prefab, positionToInstantiate, Quaternion.identity, GameObject.Find("Turrets").transform);
            prefabToInstantiate.SetActive(true);
            
            if (prefab.name.Contains("Turret"))
                AddTurretToEnemiesList(prefabToInstantiate);
    
            objectsPlaced.Add(prefabToInstantiate);

            if (objectsPlaced.Count > 0)
                lastObjectPlaced = objectsPlaced[objectsPlaced.Count - 1];
        }

        private void AddTurretToEnemiesList(GameObject turret)
        {
            foreach (var enemy in enemies)
                enemy.GetComponent<AIController>().AddTurretToList(turret);
        }

        private void RemoveTurretFromEnemiesList(GameObject turret)
        {
            foreach (var enemy in enemies)
                enemy.GetComponent<AIController>().RemoveTurret(turret);
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
            if (lastObjectPlaced != null)
            {
                objectsPlaced.Remove(objectsPlaced[objectsPlaced.Count - 1]);

                if (lastObjectPlaced.name.Contains("Turret"))
                    RemoveTurretFromEnemiesList(lastObjectPlaced);

                Destroy(lastObjectPlaced.gameObject);

                if (objectsPlaced.Count > 0)
                    lastObjectPlaced = (objectsPlaced[objectsPlaced.Count - 1]);
            }
            else
                Debug.Log("No turrets placed");
        }
    }

}