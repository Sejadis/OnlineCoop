using Runnable;
using Shared.StatusEffects;
using UnityEngine;

namespace Server.StatusEffects
{
    public class StatusEffectRunner : Runner<StatusEffect, StatusEffectRuntimeParams>
    {
        protected override StatusEffect GetRunnable(ref StatusEffectRuntimeParams runtimeParams)
        {
            return StatusEffect.CreateStatusEffect(ref runtimeParams);
        }

        protected override bool UpdateRunnable(StatusEffect runnable)
        {
            //TODO extract so expirable Runnable
            var core = base.UpdateRunnable(runnable);
            var canExpire = runnable.Description.duration > 0;
            var isExpired = canExpire && runnable.StartTime + runnable.Description.duration < Time.time;
            return core && !isExpired;
        }
    }
}