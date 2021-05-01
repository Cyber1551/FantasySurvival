﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Control
{
    public enum Stance 
    { 
        UnEquipped = 0,
        Equipped = 1
    }

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
        [SerializeField] Stance WeaponStance = Stance.UnEquipped;
        [SerializeField] float idleStanceResetTime = 5.0f;
        float stanceResetTimer = 0.0f;
        public int numberOfClicks = 0;
        float lastClickedTime = 0;
        public float maxComboDelay = 1.2f;
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
            /* if (Input.GetMouseButtonDown(0))
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

             stanceResetTimer += Time.deltaTime;*/
            PlayerCombat();
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

        public void PlayerCombat()
        {
            if (Time.time - lastClickedTime > maxComboDelay)
            {
                numberOfClicks = 0;
            }
            if (Input.GetMouseButtonDown(0))
            {
                stanceResetTimer = 0;
                lastClickedTime = Time.time;
                numberOfClicks++;
                if (numberOfClicks == 1)
                {
                    anim.SetBool("Attack1", true);
                }
                
                numberOfClicks = Mathf.Clamp(numberOfClicks, 0, 3);

            }
            if (WeaponStance == Stance.Equipped && stanceResetTimer >= idleStanceResetTime)
            {
                anim.Play("UnEquip");
                Debug.Log(stanceResetTimer);
                stanceResetTimer = 0;
                SetWeaponStance(Stance.UnEquipped);
            }
            stanceResetTimer += Time.deltaTime;

        }
        private bool IsMoving()
        {
            return moveVector.magnitude > 0;
        }
        private bool LockCamera()
        {
            return IsMoving();
        }
        public Stance GetCurrentStance()
        {
            return WeaponStance;
        }
        public void SetWeaponStance(Stance ws)
        {

            WeaponStance = ws;
            anim.SetBool("WeaponStance", ws == Stance.Equipped);
            
        }
        #region Animation Events
        private void ResetAttack()
        {
            Debug.Log("RESET ATTACK");
            blockRotationPlayer = false;
        }

        public void Return1()
        {
            if (numberOfClicks >= 2)
            {
                anim.SetBool("Attack2", true);
            }
            else
            {
                anim.SetBool("Attack1", false);
                numberOfClicks = 0;

            }
        }

        public void Return2()
        {
            if (numberOfClicks >= 3)
            {
                anim.SetBool("Attack3", true);
            }
            else
            {
                anim.SetBool("Attack1", false);
                anim.SetBool("Attack2", false);
                numberOfClicks = 0;

            }
        }
        public void Return3()
        {
            anim.SetBool("Attack1", false);
            anim.SetBool("Attack2", false);
            anim.SetBool("Attack3", false);
            numberOfClicks = 0;
        }
        #endregion
    }
}