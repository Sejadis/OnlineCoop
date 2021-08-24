using System;
using System.Collections;
using System.Collections.Generic;
using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkVariable;
using SejDev.Systems.Ability;
using UnityEngine;

public class NetworkState : NetworkBehaviour
{
    public NetworkVariableInt MaxHealth = new NetworkVariableInt(100);
    public NetworkVariableInt CurrentHealth = new NetworkVariableInt(50);

    public NetworkVariableBool IsSprinting = new NetworkVariableBool();
    public NetworkVariableString PlayerName = new NetworkVariableString();
    public NetworkVariableString IconName = new NetworkVariableString();

    public Action<Vector2> OnInputReceived;
    public Action<bool> OnSprintReceived;
    public Action<AbilityRuntimeParams> OnAbilityCast;
    public Action<AbilityType, float> OnStartCooldown;

    [ServerRpc(RequireOwnership = false)]
    public void SendInputServerRpc(Vector2 input)
    {
        OnInputReceived?.Invoke(input);
    }

    [ServerRpc(RequireOwnership = false)]
    public void ToggleSprintServerRpc(bool shouldSprint)
    {
        OnSprintReceived?.Invoke(shouldSprint);
    }

    [ServerRpc(RequireOwnership = false)]
    public void CastAbilityServerRpc(AbilityRuntimeParams runtimeParams)
    {
       OnAbilityCast?.Invoke(runtimeParams);
    }

    [ClientRpc]
    public void StartCooldownClientRpc(AbilityType type, float cooldown)
    {
        OnStartCooldown?.Invoke(type,cooldown);
    }
}