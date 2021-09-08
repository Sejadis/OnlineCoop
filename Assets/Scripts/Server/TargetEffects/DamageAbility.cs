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
            GetTarget<IDamagable>()?.Damage(EffectParameter.Actor, (int) SourceDescription.mainValue);
        }
    }
}