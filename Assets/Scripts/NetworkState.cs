using System;
using MLAPI;
using MLAPI.Messaging;
using SejDev.Systems.Ability;

public class NetworkState : NetworkBehaviour
{
    public Action<AbilityRuntimeParams> OnAbilityCast;
    [ServerRpc(RequireOwnership = false)]
    public void CastAbilityServerRpc(AbilityRuntimeParams runtimeParams)
    {
        OnAbilityCast?.Invoke(runtimeParams);
    }
}