using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Control
{
    public class ProjectileController : MonoBehaviour
    {
        [SerializeField] float timeToLive;

        private float time = 0;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (time >= timeToLive)
            {
                Destroy(gameObject);
                return;
            }

            time += Time.deltaTime;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy") || other.CompareTag("Environment"))
            {
                Destroy(gameObject);
            }
        }
    }
}