using MLAPI.Spawning;
using SejDev.Systems.Ability;

namespace Abilities
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
            NetworkSpawnManager.SpawnedObjects[abilityRuntimeParams.TargetEntity].GetComponent<IDamagable>().Damage((int) Description.mainValue);
        }

        public override bool IsBlocking()
        {
            return false;
        }
    }
}