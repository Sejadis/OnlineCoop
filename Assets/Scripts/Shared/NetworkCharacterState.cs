using System;
using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkVariable;
using Shared.Abilities;
using Shared.StatusEffects;
using UnityEngine;

namespace Shared
{
    [RequireComponent(typeof(NetworkHealthState))]
    public class NetworkCharacterState : NetworkBehaviour
    {
        private  void Awake()
        {
            NetHealthState = GetComponent<NetworkHealthState>();
        }

        public NetworkVariableBool IsSprinting = new NetworkVariableBool();
        public NetworkVariableString PlayerName = new NetworkVariableString();
        public NetworkVariableString IconName = new NetworkVariableString();
        public NetworkVariable<int[]> equippedAbilities = new NetworkVariable<int[]>();

        public event Action<Vector2> OnMoveInputReceived;
        public event Action<Vector2> OnLookInputReceived;
        public event Action<bool> OnSprintReceived;
        public event Action OnJumpReceived;
        public event Action<AbilityType, float> OnStartCooldown;
        public event Action<AbilityRuntimeParams> OnServerAbilityCast;
        public event Action<AbilityRuntimeParams> OnClientAbilityCast;
        public event Action<StatusEffectRuntimeParams> OnClientStatusEffectAdded;
    
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

        public NetworkHealthState NetHealthState { get; private set; }

        [ClientRpc]
        public void StatusEffectAddedClientRpc(StatusEffectRuntimeParams runtimeParams)
        {
            OnClientStatusEffectAdded?.Invoke(runtimeParams);
        }

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
            Vector2 v = new Vector2(1, 2);
        }

        [ClientRpc]
        public void StartCooldownClientRpc(AbilityType type, float cooldown)
        {
            OnStartCooldown?.Invoke(type,cooldown);
        }

        [ServerRpc(RequireOwnership = false)]
        public void SetJumpServerRpc()
        {
            OnJumpReceived?.Invoke();
        }
    }
}