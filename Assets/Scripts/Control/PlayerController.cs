using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Control
{
    public class PlayerController: MonoBehaviour
    {
        public float Speed;
        private Animator anim;
        private Vector3 moveVector;
        [SerializeField] private Transform cameraPivot;
        [SerializeField] private float turnSmoothTime = 0.2f;
        Vector3 desiredMoveDirection;
        float turnVel;
        [SerializeField] float desiredRotationSpeed = 0.1f;
        [SerializeField] bool blockRotationPlayer;
        [SerializeField] float allowPlayerRotationAmount = 0.3f;
        [SerializeField] bool weaponStance = false;
        [SerializeField] float idleStanceResetTime = 5.0f;
        float stanceResetTimer = 0.0f;

        InputManager input;

        float InputZ;
        float InputX;
        // Start is called before the first frame update
        void Start()
        {
            input = InputManager.Instance;
            anim = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            InputMagnitude();
            if (Input.GetMouseButtonDown(0))
            {
                stanceResetTimer = 0.0f;
                anim.SetTrigger("StopUpperAction");
                blockRotationPlayer = true;
                anim.SetTrigger("Attack");

            }
            if (weaponStance == true && stanceResetTimer >= idleStanceResetTime)
            {
                anim.SetTrigger("UnEquip");
                stanceResetTimer = 0;
                SetWeaponStance(false);
            }
            
            stanceResetTimer += Time.deltaTime;
        }

        private void FreeFormMoveAndRotation()
        {
            var camera = Camera.main;
            var forward = camera.transform.forward;
            var right = camera.transform.right;

            forward.y = 0;
            right.y = 0;

            forward.Normalize();
            right.Normalize();
            desiredMoveDirection = forward * InputZ + right * InputX;
            if (!blockRotationPlayer)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), desiredRotationSpeed);
            }
        }

        private void InputMagnitude()
        {

            InputZ = input.Vertical;
            InputX = input.Horizontal;
            //moveVector = new Vector3(InputX, 0f, InputZ).normalized;
            anim.SetFloat("X", InputX, 0.0f, Time.deltaTime * 2f);
            anim.SetFloat("Z", InputZ, 0.0f, Time.deltaTime * 2f);

            Speed = new Vector2(InputX, InputZ).sqrMagnitude;
           // Debug.Log(Speed);
            if (Speed > allowPlayerRotationAmount)
            {
                anim.SetFloat("InputMagnitude", Speed, 0.0f, Time.fixedDeltaTime);
                FreeFormMoveAndRotation();
            }
            else if (Speed < allowPlayerRotationAmount)
            {
                anim.SetFloat("InputMagnitude", Speed, 0.0f, Time.fixedDeltaTime);
            }
        }
        private bool IsMoving()
        {
            return moveVector.magnitude > 0;
        }
        private bool LockCamera()
        {
            return IsMoving();
        }

        public void SetWeaponStance(bool ws)
        {
            weaponStance = ws;
            anim.SetBool("WeaponStance", ws);
        }
        #region Animation Events
        private void ResetAttack()
        {
            Debug.Log("RESET ATTACK");
            blockRotationPlayer = false;
        }

        #endregion
    }
}