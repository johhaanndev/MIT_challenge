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

        public void Move(float x, float z)
        {
            transform.Translate(new Vector3(x, 0, z) * speedFraction);
        }

        public void Zoom(Touch touchZero, Touch touchOne)
        {

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;
            float increment = difference * zoomFraction;

            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax);
        }
    }
}