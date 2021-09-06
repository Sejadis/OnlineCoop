using MLAPI.Spawning;
using Shared;
using Shared.Abilities;
using UnityEngine;

namespace Server.Ability
{
    public class ChargedAoeAbility : AoeAbility
    {
        private bool didActivate = false;
        private float elapsedTime;

        public ChargedAoeAbility(ref AbilityRuntimeParams abilityRuntimeParams) : base(ref abilityRuntimeParams)
        {
        }

//TODO think about how cast time should work for charged abilities
        public override bool Start()
        {
            actor = NetworkSpawnManager.SpawnedObjects[AbilityRuntimeParams.Actor]
                .GetComponent<NetworkCharacterState>(); //TODO refactor into base?
            //TODO make (optionally?) follow target
            // var runtimeParams = new AbilityRuntimeParams(AbilityRuntimeParams);
            // runtimeParams.TargetPosition = AbilityRuntimeParams.StartPosition;
            actor.CastAbilityClientRpc(
                AbilityRuntimeParams); //TODO needs to happen outside of abilities, (maybe ability handler?)
            CanStartCooldown = false;
            elapsedTime = 0f; //kinda unnecessary but explicit
            return true;
        }

        public override bool Update()
        {
            elapsedTime += Time.deltaTime;
            return elapsedTime < Description.castTime;
        }

        public override bool Reactivate()
        {
            var chargeProgress = elapsedTime / Description.castTime;
            chargeProgress = Mathf.Clamp01(chargeProgress);
            var size = chargeProgress * Description.size;
            Debug.DrawRay(AbilityRuntimeParams.TargetPosition, Vector3.up * size, Color.red, 10f);
            RunHitCheck(size);
            actor.CastAbilityClientRpc(AbilityRuntimeParams);
            didActivate = true;
            CanStartCooldown = true;
            return false;
        }

        public override void End()
        {
            if (!didActivate)
            {
                Reactivate();
            }
        }

        protected override void RunHitEffects()
        {
            var chargeProgress = elapsedTime / Description.castTime;

            RunHitEffects(AbilityRuntimeParams.Targets,
                AbilityRuntimeParams.TargetPosition,
                AbilityRuntimeParams.TargetDirection,
                AbilityRuntimeParams.StartPosition,
                null,
                Description.valueModifier.Evaluate(chargeProgress));
        }
    }
}