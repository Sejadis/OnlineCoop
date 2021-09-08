namespace Server.TargetEffects
{
    public class DestroyAbility : TargetEffect
    {
        public DestroyAbility(TargetEffectParameter targetEffectParameter) : base(targetEffectParameter)
        {
        }

        public override void Run()
        {
            GetTarget().Despawn(true);
        }
    }
}