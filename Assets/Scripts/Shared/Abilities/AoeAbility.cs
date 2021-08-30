using MLAPI;
using MLAPI.Spawning;
using Server.Ability;
using UnityEngine;

namespace Shared.Abilities
{
    public class AoeAbility : Ability
    {
        private Collider[] overlapResults = new Collider[10];
        protected NetworkCharacterState actor;
        protected bool didStart { get; set; }

        public AoeAbility(ref AbilityRuntimeParams abilityRuntimeParams) : base(ref abilityRuntimeParams)
        {
        }

        public override bool Start()
        {
            actor = NetworkSpawnManager.SpawnedObjects[abilityRuntimeParams.Actor]
                .GetComponent<NetworkCharacterState>();
            if (DidCastTimePass)
            {
                didStart = true;
                RunHitCheck();
                actor.CastAbilityClientRpc(
                    abilityRuntimeParams); //TODO needs to happen outside of abilities, (maybe ability handler?)
            }

            return false;
        }

        public override bool Update()
        {
            if (DidCastTimePass)
            {
                RunHitCheck();
                if (!didStart)
                {
                    actor.CastAbilityClientRpc(
                        abilityRuntimeParams); //TODO needs to happen outside of abilities, (maybe ability handler?)
                }
                return false;
            }

            return true;
        }

        protected void RunHitCheck(float? size = null)
        {
            var resultCount = Physics.OverlapSphereNonAlloc(abilityRuntimeParams.TargetPosition,
                size ?? Description.size,
                overlapResults);
            for (var i = 0; i < resultCount; i++)
            {
                var result = overlapResults[i];
                var netObj = result.GetComponent<NetworkObject>();
                if (netObj != null)
                {
                    if (actor != null)
                    {
                        foreach (var effect in Description.HitEffects)
                        {
                            var runtimeParams = new AbilityRuntimeParams(effect, abilityRuntimeParams.Actor,
                                netObj.NetworkObjectId, result.transform.position,
                                Vector3.zero, abilityRuntimeParams.TargetPosition);
                            actor.CastAbilityServerRpc(runtimeParams);
                        }
                    }
                    else
                    {
                        //TODO try run on target?
                    }
                }
            }
        }
    }
}