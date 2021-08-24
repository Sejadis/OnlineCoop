using System;
using System.Collections;
using System.Collections.Generic;
using MLAPI.NetworkVariable;
using UnityEngine;

public class PartyUI : MonoBehaviour
{
    [SerializeField] private PlayerFrame playerFrame;

    [SerializeField] private PlayerFrame[] partyFrames;

    private int registeredPartyMembers = 0;

    public void RegisterPlayer(string playerName, NetworkState networkState)
    {
        playerFrame.RegisterPlayer(playerName, networkState);
        playerFrame.gameObject.SetActive(true);
    }

    public void RegisterPartyMember(string playerName, NetworkState networkState)
    {
        if (registeredPartyMembers == 3)
        {
            throw new InvalidOperationException("Can not register more than 3 party members. Party Full.");
        }
        var partyFrame = partyFrames[registeredPartyMembers++];
        partyFrame.RegisterPlayer(playerName, networkState);
        partyFrame.gameObject.SetActive(true);
    }
}