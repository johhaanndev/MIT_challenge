using Game.Control;
using Game.Core;
using Game.Economy;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Movement
{

    public class Dragger : MonoBehaviour
    {
        private Vector3 positionToInstantiate;

        [SerializeField] GameEconomy gameEconomy;
        private List<GameObject> enemies = new List<GameObject>();

        private GameObject lastObjectPlaced;
        private List<GameObject> objectsPlaced = new List<GameObject>();

        void Start()
        {
            enemies = GameObject.FindGameObjectsWithTag(GameTags.ENEMY).ToList();
        }

        public void Drag(GameObject turret)
        {
            if (!gameEconomy.CanBuy(turret.GetComponent<ObjectEconomy>().GetPrice()))
            {
                return;
            }

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

        public void Drop(GameObject objectPlanning, GameObject objectPrefab)
        {
            objectPlanning.SetActive(false);
            if (!CanPlaceTurret(objectPlanning))
                return;

            GameObject prefabToInstantiate = Instantiate(objectPrefab, positionToInstantiate, Quaternion.identity, GameObject.Find("Turrets").transform);
            prefabToInstantiate.SetActive(true);

            if (objectPrefab.name.Contains("Turret"))
                AddTurretToEnemiesList(prefabToInstantiate);
    
            objectsPlaced.Add(prefabToInstantiate);
            gameEconomy.SpendMoney(objectPrefab.GetComponent<ObjectEconomy>().GetPrice());

            if (objectsPlaced.Count > 0)
                lastObjectPlaced = objectsPlaced[objectsPlaced.Count - 1];
        }

        private void AddTurretToEnemiesList(GameObject turret)
        {
            foreach (var enemy in enemies)
                enemy.GetComponent<EnemyController>().AddTurretToList(turret);
        }

        private void RemoveTurretFromEnemiesList(GameObject turret)
        {
            foreach (var enemy in enemies)
                enemy.GetComponent<EnemyController>().RemoveTurret(turret);
        }

        private bool CanPlaceTurret(GameObject objectToPlace)
        {
            var price = objectToPlace.GetComponent<ObjectEconomy>().GetPrice();
            if (!gameEconomy.CanBuy(price))
                return false;
            
            var colliders = Physics.OverlapSphere(positionToInstantiate, 2.5f);
            foreach (var collider in colliders)
            {
                if (collider.CompareTag(GameTags.PLAYER) || collider.CompareTag(GameTags.ENVIRONMENT))
                {
                    var distance = Vector3.Distance(collider.transform.position, objectToPlace.transform.position);
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
            var price = 0;

            if (lastObjectPlaced != null)
            {
                objectsPlaced.Remove(objectsPlaced[objectsPlaced.Count - 1]);

                if (lastObjectPlaced.name.Contains("Turret"))
                    RemoveTurretFromEnemiesList(lastObjectPlaced);

                gameEconomy.RefundMoney(lastObjectPlaced.GetComponent<ObjectEconomy>().GetPrice());

                Destroy(lastObjectPlaced.gameObject);

                if (objectsPlaced.Count > 0)
                    lastObjectPlaced = (objectsPlaced[objectsPlaced.Count - 1]);
            }
            else
                Debug.Log("No turrets placed");
        }
    }

}