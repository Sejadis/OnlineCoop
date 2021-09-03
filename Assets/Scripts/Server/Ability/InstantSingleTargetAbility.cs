using MLAPI;
using MLAPI.Spawning;
using Shared.Abilities;

namespace Server.Ability
{
    public class InstantSingleTargetAbility : Ability
    {
        public InstantSingleTargetAbility(ref AbilityRuntimeParams abilityRuntimeParams) : base(
            ref abilityRuntimeParams)
        {
        }

        public override bool Start()
        {
            RunHitEffects();
            return false; //TODO think about this
        }

        public override bool Update()
        {
            throw new System.NotImplementedException();
        }
    }
}