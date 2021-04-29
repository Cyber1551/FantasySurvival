using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effects
{
    public class CameraShake : MonoBehaviour
    {
        public static float shakeDuration = 0f;
        public float shakeMagnitude = 2f;
        public float dampingSpeed = 1.0f;
        Vector3 initialPosition;
        private void Start()
        {
            initialPosition = transform.position;
        }

        public static void TriggerShake()
        {
            shakeDuration = 1.0f;
        }

        private void Update()
        {
            transform.LookAt(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            if (shakeDuration > 0)
            {
                transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;

                shakeDuration -= Time.deltaTime * dampingSpeed;
            }
            else
            {
                shakeDuration = 0f;
                transform.localPosition = initialPosition;
            }
        }

    }
}