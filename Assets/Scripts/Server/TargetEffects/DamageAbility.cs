using MLAPI.Spawning;
using Shared;

namespace Server.TargetEffects
{
    public class DamageAbility : TargetEffect
    {
        public DamageAbility(TargetEffectParameter targetEffectParameter) : base(targetEffectParameter)
        {
        }

        public override void Run()
        {
            if (NetworkSpawnManager.SpawnedObjects.TryGetValue(EffectParameter.Targets[0], out var netObj))
            {
                netObj.GetComponent<IDamagable>()?.Damage(EffectParameter.Actor, (int) SourceDescription.mainValue);
            }
        }
    }
}