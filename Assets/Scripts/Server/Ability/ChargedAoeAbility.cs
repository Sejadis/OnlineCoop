using MLAPI.Spawning;
using Shared;
using Shared.Abilities;
using UnityEngine;

namespace Server.Ability
{
    public class ChargedAoeAbility : AoeAbility
    {
        private bool didActivate = false;

        public ChargedAoeAbility(ref AbilityRuntimeParams abilityRuntimeParams) : base(ref abilityRuntimeParams)
        {
        }
//TODO think about how cast time should work for charged abilities
        public override bool Start()
        {
            actor = NetworkSpawnManager.SpawnedObjects[AbilityRuntimeParams.Actor].GetComponent<NetworkCharacterState>(); //TODO refactor into base?
            var runtimeParams = new AbilityRuntimeParams(AbilityRuntimeParams);
            runtimeParams.TargetPosition = AbilityRuntimeParams.StartPosition;
            actor.CastAbilityClientRpc(AbilityRuntimeParams); //TODO needs to happen outside of abilities, (maybe ability handler?)
            CanStartCooldown = false;
            return true;
        }

        public override bool Update()
        {
            return true;
        }

        public override bool Reactivate()
        {
            var chargeProgress = (Time.time - StartTime) / Description.duration;
            chargeProgress = Mathf.Clamp01(chargeProgress);
            var size = chargeProgress * Description.size;
            Debug.DrawRay(AbilityRuntimeParams.TargetPosition,Vector3.up * size, Color.red,10f);
            RunHitCheck(size);
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
    }
}