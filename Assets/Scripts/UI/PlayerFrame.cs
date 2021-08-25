using System.Collections;
using System.Collections.Generic;
using MLAPI.NetworkVariable;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFrame : MonoBehaviour
{
    [SerializeField] private Image playerIcon;

    [SerializeField] private TMP_Text nameText;

    [SerializeField] private HealthBar healthBar;

    public void RegisterPlayer(string playerName, CharacterNetworkState networkState)
    {
        nameText.text = playerName;
        networkState.PlayerName.OnValueChanged += OnPlayerNameChanged;
        networkState.IconName.OnValueChanged += OnIconNameChanged;
        OnIconNameChanged("", networkState.IconName.Value);

        healthBar.Link(networkState.NetHealthState);
    }

    private void OnIconNameChanged(string prevValue, string newValue)
    {
        playerIcon.sprite = Resources.Load<Sprite>(newValue);
    }

    private void OnPlayerNameChanged(string prevValue, string newValue)
    {
        nameText.text = newValue;
    }
}