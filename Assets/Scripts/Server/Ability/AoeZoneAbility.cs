using UnityEngine;

namespace Shared.Abilities
{
    public class AoeZoneAbility : AoeAbility
    {
        private float nextTickTime;

        public override bool Start()
        {
            base.Start();
            nextTickTime = Time.time + Description.delay;
            return true;
        }

        public override bool Update()
        {
            if (DidCastTimePass && Time.time > nextTickTime)
            {
                nextTickTime = Time.time + Description.delay;
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