using System;
using Client.UI;
using MLAPI;
using MLAPI.Messaging;
using Server.Chat;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Shared.UI
{
    public class Chat : NetworkBehaviour
    {
        [SerializeField] private GameObject chatLinePrefab;

        [SerializeField] private GameObject chatContent;

        [SerializeField] private TMP_InputField inputField;
        // Start is called before the first frame update

        private void Update()
        {
            if (Keyboard.current.enterKey.wasPressedThisFrame && !inputField.isFocused)
            {
                inputField.Select();
                inputField.ActivateInputField();
            }
        }

        public void SendChatMessage(string message)
        {
            if (!Keyboard.current.enterKey.wasPressedThisFrame)
            {
                return;
            }
            else
            {
                if (message == string.Empty)
                {
                    return;
                }
            }
            SendMessageServerRPC(message);
            inputField.text = String.Empty;
        }

        [ServerRpc(RequireOwnership = false)]
        private void SendMessageServerRPC(string message, ServerRpcParams rpcParams = default)
        {
            if (!IsServer) return;
            if (message.StartsWith("/"))
            {
                ChatCommandHandler.RunCommand(message, rpcParams.Receive.SenderClientId);
            }
            else
            {
                AddMessageClientRPC(rpcParams.Receive.SenderClientId, message);
            }
        }

        [ClientRpc]
        private void AddMessageClientRPC(ulong senderId, string message)
        {
            if (!IsClient) return;
            var obj = Instantiate(chatLinePrefab, chatContent.transform, false);
            obj.GetComponent<ChatLine>().SetMessage(senderId, message);
        }
    }
}