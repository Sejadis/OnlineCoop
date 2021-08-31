using MLAPI.Spawning;
using Server.Ability;

namespace Shared.Abilities
{
    public class HealAbility : Ability
    {
        public HealAbility(ref AbilityRuntimeParams abilityRuntimeParams) : base(ref abilityRuntimeParams)
        {
        }

        public override bool Start()
        {
            CanStartCooldown = false;
            return true;
        }

        public override bool Update()
        {
            return false;
        }
        

        public override void End()
        {
            if (NetworkSpawnManager.SpawnedObjects.TryGetValue(AbilityRuntimeParams.TargetEntity, out var netObj))
            {
                netObj.GetComponent<IHealable>()?.Heal(AbilityRuntimeParams.Actor, (int) Description.mainValue);
            }
        }
    }
}