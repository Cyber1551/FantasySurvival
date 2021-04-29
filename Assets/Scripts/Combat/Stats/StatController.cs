using Control;
using System.Collections;
using System.Collections.Generic;
using UI.Stats;
using UnityEngine;

namespace Combat.Stats
{
    public class StatController : MonoBehaviour
    {
        public static StatController Instance;
        //public List<BaseItem> items;
        public CharacterStat MaxHealth;
        public CharacterStat HealthRegen;
        public CharacterStat Damage;
        public CharacterStat CriticalChance;
        public CharacterStat CriticalDamage;
        public CharacterStat LifeSteal;
        public CharacterStat MaxShield;
        public CharacterStat Armor;
        public CharacterStat ArmorPen;

        public StatPanel statPanel;
      
        private void Awake()
        {
            Instance = this;
        }
        private void Start()
        {
            statPanel.SetStats(MaxHealth, HealthRegen, Damage, LifeSteal, CriticalChance, CriticalDamage);
        }

        /*public void AddItem(BaseItem item)
        {
            items.Add(item);
            if(item is StatItem)
            {
                AddModifier(item as StatItem);
            }
        }
        public void AddModifier(StatItem item)
        {
            float amount = (item.IsPercent) ? item.Amount / 100 : item.Amount;
            switch (item.Stat)
            {
                case "Damage":
                    Damage.AddModifier(new StatModifier(amount, (item.IsPercent) ? StatModType.PercentMult : StatModType.Flat));
                    break;
                case "MaxHealth":
                    MaxHealth.AddModifier(new StatModifier(amount, (item.IsPercent) ? StatModType.PercentMult : StatModType.Flat));
                    GetComponent<PlayerHealth>().IncreaseMaxHp();
                    break;
                case "HealthRegen":
                    HealthRegen.AddModifier(new StatModifier(amount, (item.IsPercent) ? StatModType.PercentMult : StatModType.Flat));
                    break;
                case "CriticalChance":
                    CriticalChance.AddModifier(new StatModifier(amount, (item.IsPercent) ? StatModType.PercentMult : StatModType.Flat));
                    break;
                case "CriticalDamage":
                    CriticalDamage.AddModifier(new StatModifier(amount, (item.IsPercent) ? StatModType.PercentMult : StatModType.Flat));
                    break;
                case "LifeSteal":
                    LifeSteal.AddModifier(new StatModifier(amount, (item.IsPercent) ? StatModType.PercentMult : StatModType.Flat));
                    break;
                case "Armor":
                    Armor.AddModifier(new StatModifier(amount, (item.IsPercent) ? StatModType.PercentMult : StatModType.Flat));
                    break;
            }
            statPanel.UpdateStatValues();
            
        }
        */
        private void Update()
        {
            statPanel.gameObject.SetActive(Input.GetKey(KeyCode.Tab));
        }

    }
}