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
                Debug.Log(hit.point.y);
                positionToInstantiate = turret.transform.position;
            }
            else
            {
                turret.SetActive(false);
            }
        }

        public void Drop(GameObject turret)
        {
            turret.SetActive(false);

            Debug.Log($"Position to instantiate: {positionToInstantiate}");

            GameObject turretToInstantiate = Instantiate(turret, positionToInstantiate, Quaternion.identity, GameObject.Find("Turrets").transform);
            turretToInstantiate.SetActive(true);
        }
    }

}