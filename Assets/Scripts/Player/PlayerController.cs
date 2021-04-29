using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour
{
    [HideInInspector] public AnimationController animationController;
    //[HideInInspector] public PlayerHealth playerHealth;
   // [HideInInspector] public StatController statController;
    public GameObject testEffect;
    public int Exp;
    public int MaxExp;
    public int Level;

    float attackSpeedTimer = float.MaxValue;

    [SerializeField] private GameObject basicAttack;
    private Transform projectileSpawn;
    // Start is called before the first frame update
    void Awake()
    {
        animationController = GetComponent<AnimationController>();
       // statController = GetComponent<StatController>();
        //playerHealth = GetComponent<PlayerHealth>();
       // projectileSpawn = GameObject.Find("EffectSpawn").transform;
    }

    private void Start()
    {
        Exp = 0;
        MaxExp = 1;
        Level = 1;
        SetupDelegates();
    }
    public void SetupDelegates()
    {
        animationController.BasicAttackDelegate += BasicAttack;
    }
    public void AddExp()
    {
        Exp++;
        if (Exp >= MaxExp)
        {
            Exp = 0;
            MaxExp++;
            Level++;
        }   
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.DrawRay(transform.position, transform.forward, Color.red);
        if (Input.GetMouseButton(0))
        {
            float asd = 1f;
             if (attackSpeedTimer > asd)
            {
                PlayerMovement.LockRotation = true;
                animationController.animator.SetFloat("AttackSpeed", 4f / asd);
                animationController.animator.Play("BasicAttack");
                attackSpeedTimer = 0;
            }  
        }

        if (Input.GetKeyUp(KeyCode.E))
        {   
            //playerHealth.TakeDamage(Mathf.RoundToInt(playerHealth.MaxHealth * 0.05f), false);
        }
        attackSpeedTimer += Time.deltaTime;

    }
    private void OnTriggerEnter(Collider other)
    {
        //playerHealth.TakeDamage(10, false);
    }
    public void BasicAttack()
    {
        PlayerMovement.LockRotation = false;
    }
    /*
    public void BasicAttack()
    {
        
        GameObject bAGO = Instantiate(basicAttack, projectileSpawn.position, projectileSpawn.rotation);
        Projectile proj = bAGO.GetComponent<Projectile>();
        int damage = Mathf.RoundToInt(statController.SpellPower.Value);
        int chance = Random.Range(1, 100);
        bool isCrit = false;
        if (chance <= statController.CriticalChance.Value)
        { 
            isCrit = true;
            int bonusDamage = Mathf.RoundToInt((float)damage * (statController.CriticalDamage.Value / 100));
            damage += bonusDamage;
        }
        proj.Damage = damage;
        proj.IsCrit = isCrit;
        Destroy(bAGO, 2f);
    }
    */
    
}