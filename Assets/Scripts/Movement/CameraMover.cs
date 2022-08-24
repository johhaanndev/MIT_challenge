using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Movement
{
    public class CameraMover : MonoBehaviour
    {
        [SerializeField] float speedFraction = 1;
        [SerializeField] float zoomOutMin = 3;
        [SerializeField] float zoomOutMax = 15;
        [SerializeField] float zoomFraction = 0.01f;

        private float prevZoom;
        private float currentZoom;

        private float difference;
        private float increment;

        private Vector2 touchZeroPrevPos = new Vector2();
        private Vector2 touchOnePrevPos = new Vector2();

        public void Move(float x, float z)
        {
            transform.Translate(new Vector3(x, 0, z) * speedFraction);
        }

        public void Zoom(Touch touchZero, Touch touchOne)
        {

            touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            prevZoom = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            currentZoom = (touchZero.position - touchOne.position).magnitude;

            difference = currentZoom - prevZoom;
            increment = difference * zoomFraction;

            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax);
        }
    }
}