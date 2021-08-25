using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private NetworkHealthState networkHealthState;
        [SerializeField] private Image healthBarImage;
        private int maxHealth;
        private int currentHealth;

        private void Start()
        {
            if (networkHealthState != null)
            {
                Link(networkHealthState);
            }
        }

        public void Link(NetworkHealthState networkHealthState)
        {
            this.networkHealthState = networkHealthState;
            networkHealthState.CurrentHealth.OnValueChanged += OnCurrentHealthChanged;
            networkHealthState.MaxHealth.OnValueChanged += OnMaxHealthChanged;
            maxHealth = networkHealthState.MaxHealth.Value;
            currentHealth = networkHealthState.CurrentHealth.Value;
            SetFillAmount();
        }
        private void OnMaxHealthChanged(int prevValue, int newValue)
        {
            maxHealth = newValue;
            SetFillAmount();
        }

        private void OnCurrentHealthChanged(int prevValue, int newValue)
        {
            currentHealth = newValue;
            SetFillAmount();
        }
        
        private void SetFillAmount()
        {
            var fillValue = (float) currentHealth / maxHealth;
            healthBarImage.fillAmount = fillValue;
        }
    }
}