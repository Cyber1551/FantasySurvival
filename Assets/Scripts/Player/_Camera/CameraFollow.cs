using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Player._Camera
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform target;
        [SerializeField] private Texture2D cursor;
        public float smoothSpeed = 0.125f;
        public Vector3 offset;
        public Vector3 lookPos;

        private void LateUpdate()
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

        }
    }
}