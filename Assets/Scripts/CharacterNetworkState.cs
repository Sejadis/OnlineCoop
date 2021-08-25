using System;
using System.Collections;
using System.Collections.Generic;
using MLAPI.Messaging;
using MLAPI.NetworkVariable;
using SejDev.Systems.Ability;
using UnityEngine;

[RequireComponent(typeof(NetworkHealthState))]
public class CharacterNetworkState : NetworkState
{
    private  void Awake()
    {
        NetHealthState = GetComponent<NetworkHealthState>();
    }

    public NetworkVariableBool IsSprinting = new NetworkVariableBool();
    public NetworkVariableString PlayerName = new NetworkVariableString();
    public NetworkVariableString IconName = new NetworkVariableString();

    public Action<Vector2> OnMoveInputReceived;
    public Action<Vector2> OnLookInputReceived;
    public Action<bool> OnSprintReceived;
    public Action<AbilityType, float> OnStartCooldown;

    public NetworkHealthState NetHealthState { get; private set; }

    [ServerRpc(RequireOwnership = false)]
    public void SendMoveInputServerRpc(Vector2 input)
    {
        OnMoveInputReceived?.Invoke(input);
    }
    
    [ServerRpc(RequireOwnership = false)]
    public void SendLookInputServerRpc(Vector2 input)
    {
        OnLookInputReceived?.Invoke(input);
    }

    [ServerRpc(RequireOwnership = false)]
    public void ToggleSprintServerRpc(bool shouldSprint)
    {
        OnSprintReceived?.Invoke(shouldSprint);
    }



    [ClientRpc]
    public void StartCooldownClientRpc(AbilityType type, float cooldown)
    {
        OnStartCooldown?.Invoke(type,cooldown);
    }
}