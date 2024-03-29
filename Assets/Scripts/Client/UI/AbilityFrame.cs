﻿using Shared.Abilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Client.UI
{
    public class AbilityFrame : MonoBehaviour
    {
        [SerializeField] private Image iconImage;
        [SerializeField] private Image cooldownOverlay;
        [SerializeField] private TMP_Text cooldownText;

        private float cooldown;
        private bool isCooldownRunning;
        private float elapsedTime;
        public AbilityType AbilityType { get; private set; }

        private void Start()
        {
            cooldownOverlay.gameObject.SetActive(false);
            cooldownText.gameObject.SetActive(false);
        }

        public void SetIcon(Sprite icon)
        {
            iconImage.sprite = icon;
        }

        public void SetAbilityType(AbilityType abilityType)
        {
            AbilityType = abilityType;
        }

        public void SetCooldown(float cooldown)
        {
            this.cooldown = cooldown;
            cooldownOverlay.fillAmount = 1;
            cooldownOverlay.gameObject.SetActive(true);
            cooldownText.gameObject.SetActive(true);
            cooldownText.text = cooldown.ToString();
            isCooldownRunning = true;
            elapsedTime = 0f;
        }

        private void Update()
        {
            if (isCooldownRunning)
            {
                elapsedTime += Time.deltaTime;
                var progress = elapsedTime / cooldown;
                cooldownOverlay.fillAmount = 1 - progress;
                cooldownText.text = Mathf.CeilToInt(cooldown - elapsedTime).ToString();
                if (progress >= 1)
                {
                    isCooldownRunning = false;
                    cooldownOverlay.gameObject.SetActive(false);
                    cooldownText.gameObject.SetActive(false);
                }
            }
        }
    }
}