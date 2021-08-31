using MLAPI.Spawning;
using Server;
using Server.Ability;

namespace Shared.Abilities
{
    public class ForceMoveAbility : Ability
    {
        public ForceMoveAbility(ref AbilityRuntimeParams abilityRuntimeParams) : base(ref abilityRuntimeParams)
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
                netObj.GetComponent<ServerCharacter>().ForceMove(AbilityRuntimeParams.TargetDirection, Description.force);
            }
        }
    }
}