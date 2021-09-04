using MLAPI.Spawning;
using Shared;
using Shared.Abilities;
using UnityEngine;

namespace Server.Ability
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
            actor = NetworkSpawnManager.SpawnedObjects[AbilityRuntimeParams.Actor]
                .GetComponent<NetworkCharacterState>();
            if (DidCastTimePass)
            {
                didStart = true;
                RunHitCheck();
                actor.CastAbilityClientRpc(
                    AbilityRuntimeParams); //TODO needs to happen outside of abilities, (maybe ability handler?)
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
                        AbilityRuntimeParams); //TODO needs to happen outside of abilities, (maybe ability handler?)
                }

                return false;
            }

            return true;
        }

        protected void RunHitCheck(float? size = null)
        {
            var resultCount = Physics.OverlapSphereNonAlloc(AbilityRuntimeParams.TargetPosition,
                size ?? Description.size,
                overlapResults);
            for (var i = 0; i < resultCount; i++)
            {
                var result = overlapResults[i];
                var targets = ConvertHitToTargets(result);
                RunHitEffects(targets,
                    result.transform.position,
                    result.transform.position - AbilityRuntimeParams.TargetPosition,
                    AbilityRuntimeParams.TargetPosition);
            }
        }
    }
}