using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkVariable;
using MLAPI.Spawning;
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
            // var entity =  other.GetComponent<NetworkObject>();
            // OnHit?.Invoke(entity.NetworkObjectId);
//TODO catch self
            var netObject = other.GetComponent<NetworkState>();
            if (!IsServer || netObject == null)
            {
                return;
            }

            var actor = NetworkSpawnManager.SpawnedObjects[actorId].GetComponent<NetworkState>();
            foreach (var hitEffect in hitEffects)
            {
                var runtimeParams = new AbilityRuntimeParams(hitEffect, actorId, netObject.NetworkObjectId, Vector3.zero,
                    Vector3.zero, transform.position);

                actor.CastAbilityServerRpc(runtimeParams);
                // netObject.CastAbilityServerRpc(runtimeParams);
            }
        }
    }
}