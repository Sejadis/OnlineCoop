using System.Diagnostics;
using MLAPI;
using MLAPI.Spawning;
using Shared;
using Shared.Abilities;
using UnityEngine;

namespace Server.Ability
{
    public class ProjectileAbility : Ability
    {
        private NetworkObject projectileNetObject;
        private bool didStart;
        private Stopwatch watch = new Stopwatch();
        private NetworkCharacterState actor;

        public override bool Start()
        {
            CanStartCooldown = false;
            actor = NetworkSpawnManager.SpawnedObjects[AbilityRuntimeParams.Actor]
                .GetComponent<NetworkCharacterState>(); //TODO refactor into base?
            //TODO needs to happen outside of abilities, (maybe ability handler?)
            actor.CastAbilityClientRpc(AbilityRuntimeParams);
            if (DidCastTimePass)
            {
                FireProjectile();
            }

            return true;
        }

        public override bool Update()
        {
            if (!didStart && DidCastTimePass)
            {
                watch.Stop();
                FireProjectile();
            }

            return !didStart
                   || projectileNetObject.IsSpawned
                   && Vector3.Distance(AbilityRuntimeParams.StartPosition, projectileNetObject.transform.position) <
                   Description.range;
        }

        public override void End()
        {
            CanStartCooldown = true;
            if (projectileNetObject.IsSpawned)
            {
                projectileNetObject.Despawn(true);
            }
        }

        public override void Cancel()
        {
            base.Cancel();
            if (projectileNetObject != null && projectileNetObject.IsSpawned)
            {
                projectileNetObject.Despawn(true);
            }
        }

        public override bool Reactivate()
        {
            if (!DidCastTimePass) return true; //reactivating while still casting shouldn't do anything

            CanStartCooldown = true;
            return false;
        }

        private void FireProjectile()
        {
            didStart = true;
            var desc = Description;
            var projectile =
                Object.Instantiate(desc.Prefabs[0], AbilityRuntimeParams.StartPosition, Quaternion.identity);
            projectile.transform.forward = AbilityRuntimeParams.TargetDirection;
            var serverLogic = projectile.GetComponent<ServerProjectile>();
            serverLogic.Initialize(this);
            projectileNetObject = projectile.GetComponent<NetworkObject>();
            projectileNetObject.Spawn();
        }

        public ProjectileAbility(ref AbilityRuntimeParams abilityRuntimeParams) : base(ref abilityRuntimeParams)
        {
        }
    }
}