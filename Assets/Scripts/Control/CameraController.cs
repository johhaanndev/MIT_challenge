using Game.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Control
{

    public class CameraController : MonoBehaviour
    {
        [SerializeField] Joystick joystick;

        private CameraMover mover;

        // Start is called before the first frame update
        void Start()
        {
            mover = GetComponent<CameraMover>();
        }

        // Update is called once per frame
        void Update()
        {
            var hor = joystick.Horizontal;
            var ver = joystick.Vertical;

            if (Mathf.Abs(hor) > 0.1f || Mathf.Abs(ver) > 0.1f)
            {
                MovementBehaviour(hor, ver);
            }
        }

        private void MovementBehaviour(float hor, float ver)
        {
            mover.Move(hor, ver);
        }
    }

}