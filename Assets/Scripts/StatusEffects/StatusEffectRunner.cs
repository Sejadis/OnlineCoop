using Runnable;

namespace StatusEffects
{
    public class StatusEffectRunner : Runner<StatusEffect, StatusEffectRuntimeParams>
    {
        public override void AddRunnable(ref StatusEffectRuntimeParams runtimeParameter)
        {
            throw new System.NotImplementedException();
        }

        protected override global::StatusEffects.StatusEffect GetRunnable(ref StatusEffectRuntimeParams runtimeParams)
        {
            throw new System.NotImplementedException();
        }
    }
}