using System;
using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkVariable;
using Shared.Abilities;
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

        public Action<Vector2> OnMoveInputReceived;
        public Action<Vector2> OnLookInputReceived;
        public Action<bool> OnSprintReceived;
        public Action OnJumpReceived;
        public Action<AbilityType, float> OnStartCooldown;
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
            Vector2 v = new Vector2(1, 2);
        }

        [ClientRpc]
        public void StartCooldownClientRpc(AbilityType type, float cooldown)
        {
            OnStartCooldown?.Invoke(type,cooldown);
        }

        [ServerRpc]
        public void SetJumpServerRpc()
        {
            OnJumpReceived?.Invoke();
        }
    }
}