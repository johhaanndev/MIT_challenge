using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Movement
{
    public class CameraMover : MonoBehaviour
    {
        [SerializeField] float speedFraction = 1;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Move(float x, float z)
        {
            transform.Translate(new Vector3(x, 0, z) * speedFraction);
            //transform.localPosition += new Vector3(x, 0, z) * speedFraction;
        }
    }
}