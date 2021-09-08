using System;
using MLAPI.Spawning;
using Server.TargetEffects;
using Shared;

namespace CrowdControl
{
    public class CrowdControlAbility : TargetEffect
    {
        public CrowdControlAbility(TargetEffectParameter runtimeParams) : base(runtimeParams)
        {
        }

        public override void Run()
        {
            // GetTarget<ICrowdControl>().AddCrowdControl(EffectParameter.);
        }
    }
}