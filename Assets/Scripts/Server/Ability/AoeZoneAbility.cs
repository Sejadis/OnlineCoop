using Shared.Abilities;
using UnityEngine;

namespace Server.Ability
{
    public class AoeZoneAbility : AoeAbility
    {
        private float elapsedTime;

        public override bool Start()
        {
            base.Start();
            return true;
        }

        public override bool Update()
        {
            elapsedTime += Time.deltaTime;
            if (DidCastTimePass && Description.delay <= elapsedTime)
            {
                RunHitCheck();
                if (!didStart)
                {
                    actor.CastAbilityClientRpc(AbilityRuntimeParams);
                }
            }

            return true;
        }

        public AoeZoneAbility(ref AbilityRuntimeParams abilityRuntimeParams) : base(ref abilityRuntimeParams)
        {
        }
    }
}