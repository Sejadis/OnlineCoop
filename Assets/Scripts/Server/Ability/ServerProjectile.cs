using System;
using MLAPI;
using MLAPI.Spawning;
using Server;
using Server.Ability;
using UnityEngine;

namespace Shared.Abilities
{
    public class ServerProjectile : NetworkBehaviour
    {
        private AbilityDescription abilityDescription;

        private bool isInitialized = false;
        private float startTime;
        private float lifeTime;
        private ulong actorId;

        public override void NetworkStart()
        {
            base.NetworkStart();
            if (!IsServer)
            {
                enabled = false;
                return;
            }
        }

        public void Initialize(AbilityDescription description, ulong actorId)
        {
            abilityDescription = description;
            lifeTime = description.range / description.speed;
            startTime = Time.time;
            isInitialized = true;
            this.actorId = actorId;
        }

        private void OnTriggerEnter(Collider other)
        {
            // var entity =  other.GetComponent<NetworkObject>();
            // OnHit?.Invoke(entity.NetworkObjectId);
            //TODO refactor to decrease duplicated code (like server aoe zone)
//TODO catch self (target mask)
            var netObject = other.GetComponent<NetworkObject>();
            if (!IsServer || other.gameObject.name == "PlayerPrefab(Clone)" || netObject == null)
            {
                return;
            }

            if (other.TryGetComponent(out ServerCharacter serverChar))
            {
                // var hitDirection = other.transform.position - transform.position;
                var hitDirection = transform.forward;
                hitDirection.y = 0;

                serverChar.ForceMove(other.transform.position + hitDirection.normalized *2, abilityDescription.force) ;
            }

            var actor = NetworkSpawnManager.SpawnedObjects[actorId].GetComponent<NetworkCharacterState>();
            foreach (var hitEffect in abilityDescription.HitEffects)
            {
                var runtimeParams = new AbilityRuntimeParams(abilityDescription.abilityType, actorId, netObject.NetworkObjectId,
                    Vector3.zero,
                    Vector3.zero, transform.position, hitEffect);

                actor.CastAbilityServerRpc(runtimeParams);
                // netObject.CastAbilityServerRpc(runtimeParams);
            }
        }

        private void Update()
        {
            if (!isInitialized)
            {
                return;
            }

            // if (Time.time > startTime + lifeTime)
            // {
            //     GetComponent<NetworkObject>().Despawn(true);
            // }

            var transf = transform;
            var move = transf.forward * (Time.deltaTime * abilityDescription.speed);
            transf.position += move;
        }
    }
}