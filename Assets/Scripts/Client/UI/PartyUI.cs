using System;
using Shared;
using UnityEngine;

namespace Client.UI
{
    public class PartyUI : MonoBehaviour
    {
        [SerializeField] private PlayerFrame playerFrame;

        [SerializeField] private PlayerFrame[] partyFrames;

        private int registeredPartyMembers = 0;

        public void RegisterPlayer(string playerName, NetworkCharacterState state)
        {
            playerFrame.RegisterPlayer(playerName, state);
            playerFrame.gameObject.SetActive(true);
        }

        public void RegisterPartyMember(string playerName, NetworkCharacterState state)
        {
            if (registeredPartyMembers == 3)
            {
                throw new InvalidOperationException("Can not register more than 3 party members. Party Full.");
            }
            var partyFrame = partyFrames[registeredPartyMembers++];
            partyFrame.RegisterPlayer(playerName, state);
            partyFrame.gameObject.SetActive(true);
        }
    }
}