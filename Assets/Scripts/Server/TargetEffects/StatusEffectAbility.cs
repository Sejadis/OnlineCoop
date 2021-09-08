using MLAPI.Spawning;
using Shared;
using Shared.StatusEffects;

namespace Server.TargetEffects
{
    public class StatusEffectAbility : TargetEffect
    {
        public StatusEffectAbility(TargetEffectParameter targetEffectParameter) : base(targetEffectParameter)
        {
        }

        public override void Run()
        {
            //TODO refactor, move creation inside, switch away from serverchar as parameter
            var runtimeParams = new StatusEffectRuntimeParams();
            runtimeParams.source = EffectParameter.Actor;
            runtimeParams.EffectType = EffectParameter.StatusEffectType;
            runtimeParams.targets = EffectParameter.Targets;
            GetTarget<IBuffable>()?.AddStatusEffect(ref runtimeParams);
        }
    }
}