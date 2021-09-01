using DefaultNamespace;
using MLAPI.Spawning;
using Server.Ability;

namespace Shared.Abilities
{
    public class DestroyAbility : AbilityTargetEffect
    {
        public DestroyAbility(ref AbilityRuntimeParams abilityRuntimeParams) : base(ref abilityRuntimeParams)        {
        }

        public override bool Start()
        {
            if(NetworkSpawnManager.SpawnedObjects.TryGetValue(AbilityRuntimeParams.TargetEntity, out var obj))
            {
               obj.Despawn(true); 
            }
            return false;
        }
    }
}