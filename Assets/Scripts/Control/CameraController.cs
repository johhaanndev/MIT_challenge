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
            if (Input.touchCount == 2)
            {
                Touch touch0 = Input.GetTouch(0);
                Touch touch1 = Input.GetTouch(1);

                ZoomBehaviour(touch0, touch1);

                return;
            }

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

        private void ZoomBehaviour (Touch touch0, Touch touch1)
        {
            mover.Zoom(touch0, touch1);
        }
    }

}