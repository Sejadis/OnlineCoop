using Server.Ability;
using UnityEngine;

namespace Shared.Abilities
{
    public class LogAbility : Ability
    {
        public LogAbility(ref AbilityRuntimeParams abilityRuntimeParams) : base(ref abilityRuntimeParams)
        {
        }

        public override bool Start()
        {
            Debug.Log("Log message triggered");
            return false;
        }

        public override bool Update()
        {
            return false;
        }
    }
}