using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] float Speed;
        [SerializeField] float RotSpeed;
        private void Start()
        {
        }
        // Update is called once per frame
        void Update()
        {
            var targetVector = new Vector3(InputManager.Instance.Horizontal, 0, InputManager.Instance.Vertical);
            Move(targetVector);
            Rotate(targetVector);
        }

        private void Move(Vector3 targetVector)
        {
            var speed = Speed * Time.deltaTime;
            var pos = transform.position + targetVector * speed;
            transform.position = pos;
        }
        private void Rotate(Vector3 targetVector)
        {
            if (targetVector.magnitude == 0) return;
            var rot = Quaternion.LookRotation(targetVector);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, RotSpeed);
        }
    }
}