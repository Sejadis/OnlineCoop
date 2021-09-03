using MLAPI.Spawning;
using Shared;

namespace Server.TargetEffects
{
    public class HealAbility : TargetEffect
    {
        public HealAbility(TargetEffectParameter targetEffectParameter) : base(targetEffectParameter)
        {
        }

        public override void Run()
        {
            if (NetworkSpawnManager.SpawnedObjects.TryGetValue(EffectParameter.Targets[0], out var netObj))
            {
                netObj.GetComponent<IHealable>()?.Heal(EffectParameter.Actor, (int) SourceDescription.mainValue);
            }
        }
    }
}