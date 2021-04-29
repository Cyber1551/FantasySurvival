using Combat.Stats;
using Core;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Utility;

namespace UI.Stats
{
    public class StatDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private CharacterStat _characterStat;
        public CharacterStat Stat 
        { 
            get { return _characterStat; } 
            set
            { 
                _characterStat = value; 
                UpdateStatValue(); 
            }
        }

        public void UpdateStatValue()
        {
            StatController statController = GameAssets.I.player.GetComponent<StatController>();
            valueText.text = decimal.Round((decimal)_characterStat.Value, 2).ToString();
            if (_characterStat == statController.LifeSteal || _characterStat == statController.HealthRegen || _characterStat == statController.CriticalChance || _characterStat == statController.CriticalDamage)
            {
                valueText.text += '%';
            }
            
        }

        private string _name;
        public string Name { get { return _name; } set { _name = value; } }


        //[SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI valueText;

        [SerializeField] Tooltip tooltip;

        private void OnValidate()
        {
            TextMeshProUGUI[] texts = GetComponentsInChildren<TextMeshProUGUI>();
            //nameText = texts[0];
            valueText = texts[0];
            if (tooltip == null)
            {
                tooltip = FindObjectOfType<Tooltip>();
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            tooltip.ShowTooltip_Stat(Stat, Name);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            tooltip.HideTooltip();
        }

    }
}
