using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance;

        public KeyCode InventoryPanel = KeyCode.I;
        public KeyCode CharacterPanel = KeyCode.C;
        public KeyCode Escape = KeyCode.Escape;

        public float Horizontal { get; private set; }
        public float Vertical { get; private set; }
        public float MouseX { get; private set; }
        public float MouseY { get; private set; }
        public bool Action { get; private set; }
        public Vector3 lookDir;

        private Vector3 lookPos;

        public void Awake()
        {
            Instance = this;
        }
        // Update is called once per frame
        private void Update()
        {
            Horizontal = Input.GetAxis("Horizontal");
            Vertical = Input.GetAxis("Vertical");
            MouseX = Input.GetAxis("Mouse X");
            MouseY = Input.GetAxis("Mouse Y");
            Action = Input.GetMouseButtonUp(0);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (Vector3.Distance(transform.position, hit.point) > 1.5)
                {
                    lookPos = hit.point;
                }
            }
            lookDir = lookPos - transform.position;
            lookDir.y = 0;
        }
    }
}