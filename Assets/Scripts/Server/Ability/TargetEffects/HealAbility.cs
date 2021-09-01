using DefaultNamespace;
using MLAPI.Spawning;
using Server.Ability;

namespace Shared.Abilities
{
    public class HealAbility : AbilityTargetEffect
    {
        public HealAbility(ref AbilityRuntimeParams abilityRuntimeParams) : base(ref abilityRuntimeParams)
        {
        }

        public override bool Start()
        {
            if (NetworkSpawnManager.SpawnedObjects.TryGetValue(AbilityRuntimeParams.TargetEntity, out var netObj))
            {
                netObj.GetComponent<IHealable>()?.Heal(AbilityRuntimeParams.Actor, (int)Description.mainValue);
            }
            return false;
        }
    }
}