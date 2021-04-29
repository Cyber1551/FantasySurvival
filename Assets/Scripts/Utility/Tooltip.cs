using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Text;
using UnityEngine.UI;
using Combat.Stats;

namespace Utility
{
    public class Tooltip : MonoBehaviour
    {
        public static Tooltip Instance;
        private TextMeshProUGUI tooltipText;
        
        private StringBuilder sb = new StringBuilder();
        private void Awake()
        {
            Instance = this;
        }
        private void Start()
        {
            tooltipText = transform.GetComponentInChildren<TextMeshProUGUI>();
            gameObject.SetActive(false);
            
        }
        
        public void ShowTooltip(string title, string body)
        {
            string tooltipString = $"<b>{title}</b><br>{body}";
            tooltipText.text = tooltipString;
            gameObject.SetActive(true);
        }


        public void ShowTooltip_Stat(CharacterStat stat, string statName)
        {
            sb.Length = 0;
            string op = (stat.Value > stat.BaseValue) ? "+" : "-";
            sb.Append($"{statName} {decimal.Round((decimal)(stat.Value), 2)} ({stat.BaseValue} {op} {decimal.Round((decimal)(Mathf.Abs(stat.Value - stat.BaseValue)), 2)})");
            string title = sb.ToString();
            string desc = stat.Description;

            string body = $"<i>{desc}</i>"; //+ GetStatModifiersText(stat);
            ShowTooltip(title, body);
        }
        
        
        private void AddStat(float value, string statName, bool isPercent = false)
        {
            if (value != 0)
            {
                if (sb.Length > 0)
                    sb.AppendLine();
                if (value > 0)
                    sb.Append("+");

                sb.Append(value);
                if (isPercent)
                {
                    sb.Append("% ");
                }
                else
                {
                    sb.Append(" ");
                }
                
                sb.Append(statName);
            }
        }

        
        public void HideTooltip()
        {
            gameObject.SetActive(false);
        }
        
    }

}