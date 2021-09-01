using DefaultNamespace;
using MLAPI.Spawning;
using Server.Ability;

namespace Shared.Abilities
{
    public class DamageAbility : AbilityTargetEffect
    {
        public DamageAbility(ref AbilityRuntimeParams abilityRuntimeParams) : base(ref abilityRuntimeParams)
        {
        }

        public override bool Start()
        {
            if (NetworkSpawnManager.SpawnedObjects.TryGetValue(AbilityRuntimeParams.TargetEntity, out var netObj))
            {
                netObj.GetComponent<IDamagable>()?.Damage(AbilityRuntimeParams.Actor, (int) Description.mainValue);
            }
            return false;
        }
    }
}