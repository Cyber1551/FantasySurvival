
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Control
{
    public enum SwordMode
    {
        Sheathed,
        Stance1,
        Stance2
    }
     
    public class PlayerControllerOne : MonoBehaviour
    {
        public float WalkSpeed;
        public float RunSpeed;
        public bool IsRunning;
        public bool IsMoving;
        public float SheathWaitTime;
        public AudioClip[] sounds;
        public AudioSource source;
        public float CurrentSheathTime;

        public SwordMode CurrentSwordMode = SwordMode.Sheathed;
        private Vector3 moveDirection;
        private float inputMagnitude;
        public int numberOfClicks = 0;
        float lastClickedTime = 0;
        public float maxComboDelay = 1.2f;

        public CapsuleCollider sword;

        Animator anim;

        private void Start()
        {
            anim = GetComponentInChildren<Animator>();
            source = GetComponent<AudioSource>();
        }

        void Update()
        {
            PlayerMovement();
            HandleSwordMode();
            PlayerCombat();

           
        }
        private void PlayerMovement()
        {
            if (CurrentSwordMode != SwordMode.Stance2)
            { 
                float horizontal = Input.GetAxis("Horizontal");
                float vertical = Input.GetAxis("Vertical");
                IsRunning = Input.GetKey(KeyCode.LeftShift);
                moveDirection = new Vector3(horizontal, 0, vertical);
                inputMagnitude = moveDirection.magnitude;
                IsMoving = (horizontal != 0) || (vertical != 0);
                float Speed = (IsRunning) ? RunSpeed : WalkSpeed;
                //transform.Translate(moveDirection * Speed * Time.deltaTime, Space.Self);
                anim.SetFloat("Horizontal", horizontal);
                anim.SetFloat("Vertical", vertical);
                anim.SetFloat("Velocity", inputMagnitude);
                anim.SetBool("IsMoving", IsMoving);
            } 
            else
            {
                anim.SetFloat("Horizontal", 0);
                anim.SetFloat("Vertical", 0);
                anim.SetFloat("Velocity", 0);
                anim.SetBool("IsMoving", false);
            }
        }
        public void HandleSwordMode()
        {
            if (CurrentSwordMode != SwordMode.Sheathed)
            {
                //Debug.Log(CurrentSwordMode);
                anim.SetInteger("SwordMode", (int)CurrentSwordMode);
                if (!IsMoving)
                {
                    CurrentSheathTime -= Time.deltaTime;
                    if (CurrentSheathTime <= 0.0f)
                    {
                        anim.Play("Sheath Sword");
                        CurrentSwordMode = SwordMode.Sheathed;
                        CurrentSheathTime = SheathWaitTime;
                    }
                }
                else
                {
                    CurrentSheathTime = SheathWaitTime;
                }


            }
            else if (CurrentSwordMode == SwordMode.Sheathed)
            {
                anim.SetInteger("SwordMode", (int)CurrentSwordMode);
                if (IsMoving)
                {
                    anim.Play("Equip Sword");
                    CurrentSwordMode = SwordMode.Stance1;
                }
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
                
                if (CurrentSwordMode == SwordMode.Sheathed)
                {
                    anim.SetInteger("SwordMode", (int)CurrentSwordMode);
                    anim.Play("Equip Sword");
                    CurrentSwordMode = SwordMode.Stance1;
                } 
                else 
                {

                    CurrentSheathTime = SheathWaitTime;
                    lastClickedTime = Time.time;
                    numberOfClicks++;
                    if (numberOfClicks == 1)
                    {
                       
                        anim.SetBool("Attack1", true);
                    }

                    numberOfClicks = Mathf.Clamp(numberOfClicks, 0, 3);
                }
                

            }

            if(Input.GetMouseButtonDown(1))
            {
                
                if (numberOfClicks > 0) return;
                switch (CurrentSwordMode)
                {
                    case SwordMode.Stance1:
                        SwitchFromStance1ToStance2();
                        break;
                    case SwordMode.Stance2:
                        SwitchFromStance2ToStance1();
                        break;

                }
                
            }
       }
        public void EnableCollider(float DamagePercent)
        {

            source.clip = GetRandomSound();
            source.Play();
            //sword.transform.GetComponent<Weapon>().DamagePercent = DamagePercent;
            sword.enabled = true;
        }
         public AudioClip GetRandomSound()
        {
            return sounds[Random.Range(0, sounds.Length)];
        }
        public void DisableCollider()
        {
            sword.enabled = false;
        }
        public void SwitchFromStance1ToStance2()
        {
            CurrentSheathTime = SheathWaitTime;
            anim.SetTrigger("SwitchToStance2");
            Return3();
        }
        public void SwitchFromStance2ToStance1()
        {
            CurrentSheathTime = SheathWaitTime;
            anim.SetTrigger("SwitchToStance1");
            Return3();
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.CompareTag("Enemy"))
            {
                //InitDamage(other);
            }
            
        }
        /*private void InitDamage(Collider other)
        {
            EnemyHealth stats = other.gameObject.GetComponentInParent<EnemyHealth>();
            float percent = other.gameObject.GetComponent<Weapon>().DamagePercent;
            int damage = Mathf.RoundToInt(stats.Damage * percent);
            int chance = Random.Range(0, 100);
            bool isCrit = false;
            if (chance <= stats.CriticalChance.Value)
            {

                isCrit = true;
                int bonusDamage = Mathf.RoundToInt((float)damage * (stats.CriticalDamage.Value / 100));
                damage += bonusDamage;
            }
            float ls = stats.LifeSteal.Value;

            if (ls > 0 && Player.Hp < stats.MaxHealth.Value)
            {
                int lsAmount = Mathf.RoundToInt((ls / 100) * damage);
                if (lsAmount > 0)
                {
                    Player.HealDamage(lsAmount, isCrit);
                }

            }
            GetComponent<PlayerHealth>().TakeDamage(damage, false);
        }*/
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
         
    }
}
