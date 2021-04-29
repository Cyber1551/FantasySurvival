using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Control
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private GameObject Target;
        [SerializeField] private Vector3 Offset;
        [SerializeField] private float Smoothing;
        [SerializeField] private Vector2 PitchMinMax = new Vector2(-40, 85);
        private Camera cam;
        public bool LockCursor;

        [SerializeField] private Vector2 RotationSpeed;
        private float Pitch;
        private float Yaw;

        // Update is called once per frame
        private void Start()
        {
            cam = transform.GetChild(0).GetComponent<Camera>();
            cam.transform.localPosition = Offset;
            if (LockCursor)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }

        }
        void LateUpdate()
        {
            transform.position = Target.transform.position;
            Yaw += RotationSpeed.x * Input.GetAxis("Mouse X");
            Pitch -= RotationSpeed.y * Input.GetAxis("Mouse Y");
            Pitch = Mathf.Clamp(Pitch, PitchMinMax.x, PitchMinMax.y);
            transform.eulerAngles = new Vector3(Pitch, Yaw, 0.0f);
        }

    }
}