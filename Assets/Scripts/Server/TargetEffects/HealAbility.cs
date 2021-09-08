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
            GetTarget<IHealable>()?.Heal(EffectParameter.Actor, (int) SourceDescription.mainValue);
        }
    }
}