using MLAPI.Spawning;
using TreeEditor;
using UnityEngine;

namespace Shared.Abilities
{
    public class ChargedAoeAbility : AoeAbility
    {
        private bool didActivate = false;

        public ChargedAoeAbility(ref AbilityRuntimeParams abilityRuntimeParams) : base(ref abilityRuntimeParams)
        {
        }

        public override bool Start()
        {
            actor = NetworkSpawnManager.SpawnedObjects[abilityRuntimeParams.Actor].GetComponent<NetworkCharacterState>(); //TODO refactor into base?
            var runtimeParams = new AbilityRuntimeParams(abilityRuntimeParams);
            runtimeParams.TargetPosition = abilityRuntimeParams.StartPosition;
            actor.CastAbilityClientRpc(abilityRuntimeParams); //TODO needs to happen outside of abilities, (maybe ability handler?)
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
            Debug.DrawRay(abilityRuntimeParams.TargetPosition,Vector3.up * size, Color.red,10f);
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