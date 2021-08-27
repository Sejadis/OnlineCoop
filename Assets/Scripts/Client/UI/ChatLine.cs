using TMPro;
using UnityEngine;

namespace Client.UI
{
    public class ChatLine : MonoBehaviour
    {
        [SerializeField] private TMP_Text messageText;

        public void SetMessage(ulong sender, string message)
        {
            messageText.text = $"[{sender}]: {message}";
        }

    }
}
