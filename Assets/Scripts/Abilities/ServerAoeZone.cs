using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkVariable;
using SejDev.Systems.Ability;
using UnityEngine;

namespace Abilities
{
    public class ServerAoeZone : NetworkBehaviour
    {
        private NetworkVariableFloat scale = new NetworkVariableFloat();
        private AbilityType[] hitEffects;
        private ulong actorId;

        public override void NetworkStart()
        {
            base.NetworkStart();
            if (!IsServer)
            {
                SetVisuals();
                enabled = false;
                return;
            }

            SetVisualsClientRpc();
        }

        public void Initialize(float duration, float range, AbilityType[] hitEffects, ulong actorId)
        {
            scale.Value = range / 2;
            // transform.localScale *= scale;
            // SetVisualsClientRpc(scale);
            this.hitEffects = hitEffects;
            this.actorId = actorId;
        }

        [ClientRpc]
        private void SetVisualsClientRpc()
        {
            SetVisuals();
        }

        private void SetVisuals()
        {
            transform.localScale *= scale.Value;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!IsServer)
            {
                return;
            }

            foreach (var hitEffect in hitEffects)
            {
                var netState = other.GetComponent<NetworkState>();
                var runtimeParams = new AbilityRuntimeParams(hitEffect, actorId, netState.NetworkObjectId, Vector3.zero,
                    Vector3.zero, transform.position);
                netState.CastAbilityServerRpc(runtimeParams);
            }
        }
    }
}