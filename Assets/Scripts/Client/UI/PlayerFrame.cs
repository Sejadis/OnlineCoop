using Shared;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Client.UI
{
    public class PlayerFrame : MonoBehaviour
    {
        [SerializeField] private Image playerIcon;

        [SerializeField] private TMP_Text nameText;

        [SerializeField] private HealthBar healthBar;

        public void RegisterPlayer(string playerName, NetworkCharacterState state)
        {
            nameText.text = playerName;
            state.PlayerName.OnValueChanged += OnPlayerNameChanged;
            state.IconName.OnValueChanged += OnIconNameChanged;
            OnIconNameChanged("", state.IconName.Value);

            healthBar.Link(state.NetHealthState);
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
}