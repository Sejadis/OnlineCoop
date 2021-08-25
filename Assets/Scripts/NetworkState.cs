using System;
using MLAPI;
using MLAPI.Messaging;
using SejDev.Systems.Ability;

public class NetworkState : NetworkBehaviour
{
    public Action<AbilityRuntimeParams> OnServerAbilityCast;
    public Action<AbilityRuntimeParams> OnClientAbilityCast;
    
    [ServerRpc(RequireOwnership = false)]
    public void CastAbilityServerRpc(AbilityRuntimeParams runtimeParams)
    {
        OnServerAbilityCast?.Invoke(runtimeParams);
    }

    [ClientRpc]
    public void CastAbilityClientRpc(AbilityRuntimeParams runtimeParams)
    {
        OnClientAbilityCast?.Invoke(runtimeParams);
    }
}