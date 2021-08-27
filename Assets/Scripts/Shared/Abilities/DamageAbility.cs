using MLAPI.Spawning;
using Server.Ability;

namespace Shared.Abilities
{
    public class DamageAbility : Ability
    {
        public DamageAbility(ref AbilityRuntimeParams abilityRuntimeParams) : base(ref abilityRuntimeParams)
        {
        }

        public override bool Start()
        {
            return true;
        }

        public override bool Update()
        {
            return false;
        }

        public override void End()
        {
            if (NetworkSpawnManager.SpawnedObjects.TryGetValue(abilityRuntimeParams.TargetEntity, out var netObj))
            {
                netObj.GetComponent<IDamagable>().Damage((int) Description.mainValue);
            }
        }

        public override bool IsBlocking()
        {
            return false;
        }
    }
}