using SejDev.Systems.Ability;
using UnityEngine;

namespace Abilities
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

        public override void End()
        {
        }

        public override bool IsBlocking()
        {
            return false;
        }
    }
}