using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Movement
{

    public class Dragger : MonoBehaviour
    {
        private Vector3 positionToInstantiate;

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
            var colliders = Physics.OverlapSphere(turretPlanning.transform.position, 2f);
            foreach(var collider in colliders)
            {
                if (collider.CompareTag("Player"))
                {
                    Debug.Log("Collides with another turret");
                    return;
                }
            }

            GameObject turretToInstantiate = Instantiate(turretPrefab, positionToInstantiate, Quaternion.identity, GameObject.Find("Turrets").transform);
            turretToInstantiate.SetActive(true);
        }
    }

}