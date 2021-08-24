using System.Collections;
using System.Collections.Generic;
using MLAPI.NetworkVariable;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFrame : MonoBehaviour
{
    [SerializeField] private Image playerIcon;

    [SerializeField] private TMP_Text nameText;

    [SerializeField] private Image healthBarImage;
    private int maxHealth;
    private int currentHealth;

    public void RegisterPlayer(string playerName, NetworkState networkState)
    {
        nameText.text = playerName;
        networkState.CurrentHealth.OnValueChanged += OnCurrentHealthChanged;
        networkState.MaxHealth.OnValueChanged += OnMaxHealthChanged;
        networkState.PlayerName.OnValueChanged += OnPlayerNameChanged;
        networkState.IconName.OnValueChanged += OnIconNameChanged;
        maxHealth = networkState.MaxHealth.Value;
        currentHealth = networkState.CurrentHealth.Value;
        SetFillAmount();
        OnIconNameChanged("", networkState.IconName.Value);
    }

    private void OnIconNameChanged(string prevValue, string newValue)
    {
        playerIcon.sprite = Resources.Load<Sprite>(newValue);
    }

    private void OnPlayerNameChanged(string prevValue, string newValue)
    {
        nameText.text = newValue;
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