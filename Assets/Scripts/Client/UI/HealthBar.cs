using System;
using Client.UI.Types;
using Shared;
using Shared.Settings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Client.UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private NetworkHealthState networkHealthState;
        [SerializeField] private Image healthBarImage;
        [SerializeField] private TextMeshProUGUI healthText;
        [SerializeField] private SettingHealthDisplayMode healthDisplayMode;
        private int maxHealth;
        private int currentHealth;
        private HealthDisplayMode displayMode;

        private void Start()
        {
            if (networkHealthState != null)
            {
                Link(networkHealthState);
            }

            displayMode = healthDisplayMode.Value;
            healthDisplayMode.OnValueChanged += (_, newValue) =>
            {
                displayMode = newValue;
                UpdateUI();
            };
            UpdateUI();
        }

        public void Link(NetworkHealthState networkHealthState)
        {
            this.networkHealthState = networkHealthState;

            networkHealthState.CurrentHealth.OnValueChanged += OnCurrentHealthChanged;
            networkHealthState.MaxHealth.OnValueChanged += OnMaxHealthChanged;
            maxHealth = networkHealthState.MaxHealth.Value;
            currentHealth = networkHealthState.CurrentHealth.Value;
            UpdateUI();
        }

        private void OnMaxHealthChanged(int prevValue, int newValue)
        {
            maxHealth = newValue;
            UpdateUI();
        }

        private void OnCurrentHealthChanged(int prevValue, int newValue)
        {
            currentHealth = newValue;
            UpdateUI();
        }

        private void UpdateUI()
        {
            SetHealthText();
            SetFillAmount();
        }

        private void SetHealthText()
        {
            switch (displayMode)
            {
                case HealthDisplayMode.None:
                    healthText.enabled = false;
                    break;
                case HealthDisplayMode.Current:
                    healthText.text = currentHealth.ToString();
                    healthText.enabled = true;
                    break;
                case HealthDisplayMode.Percent:
                    healthText.text = $"{((float) currentHealth / maxHealth * 100f)}%";
                    healthText.enabled = true;
                    break;
                case HealthDisplayMode.CurrentAndTotal:
                    healthText.text = $"{currentHealth} / {maxHealth}";
                    healthText.enabled = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void SetFillAmount()
        {
            var fillValue = (float) currentHealth / maxHealth;
            healthBarImage.fillAmount = fillValue;
        }
    }
}