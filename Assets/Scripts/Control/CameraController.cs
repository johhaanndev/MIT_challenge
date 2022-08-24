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

        private Touch touch0;
        private Touch touch1;

        private float hor;
        private float ver;

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
                touch0 = Input.GetTouch(0);
                touch1 = Input.GetTouch(1);

                ZoomBehaviour(touch0, touch1);

                return;
            }

            hor = joystick.Horizontal;
            ver = joystick.Vertical;

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