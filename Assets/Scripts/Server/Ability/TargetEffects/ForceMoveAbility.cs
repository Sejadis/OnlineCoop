using DefaultNamespace;
using MLAPI.Spawning;
using Server;
using Server.Ability;
using Shared.Data;

namespace Shared.Abilities
{
    public class ForceMoveAbility : AbilityTargetEffect
    {
        public ForceMoveAbility(ref AbilityRuntimeParams abilityRuntimeParams) : base(ref abilityRuntimeParams)
        {
        }

        public override bool Start()
        {
            if (NetworkSpawnManager.SpawnedObjects.TryGetValue(AbilityRuntimeParams.TargetEntity, out var netObj))
            {
                netObj.GetComponent<ServerCharacter>()?.ForceMove(AbilityRuntimeParams.TargetDirection, Description.force);
            }
            return false;
        }
    }
}