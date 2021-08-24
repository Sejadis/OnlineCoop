using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatLine : MonoBehaviour
{
    [SerializeField] private TMP_Text messageText;

    public void SetMessage(ulong sender, string message)
    {
        messageText.text = $"[{sender}]: {message}";
    }

}
