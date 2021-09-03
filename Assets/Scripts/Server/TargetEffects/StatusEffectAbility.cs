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
            if (NetworkSpawnManager.SpawnedObjects.TryGetValue(EffectParameter.Target, out var netObj))
            {
                //TODO refactor, move creation inside, switch away from serverchar as parameter
                var runtimeParams = new StatusEffectRuntimeParams();
                runtimeParams.source = EffectParameter.Actor;
                runtimeParams.EffectType = EffectParameter.StatusEffectType;
                runtimeParams.target = EffectParameter.Target;
                netObj.GetComponent<IBuffable>()?.AddStatusEffect(ref runtimeParams);
            }
        }
    }
}