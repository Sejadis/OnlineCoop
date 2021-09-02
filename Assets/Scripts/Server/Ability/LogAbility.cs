using Shared.Abilities;
using UnityEngine;

namespace Server.Ability
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