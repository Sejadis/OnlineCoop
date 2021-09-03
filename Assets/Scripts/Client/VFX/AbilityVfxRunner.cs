using System.Linq;
using Runnable;
using Shared;
using Shared.Abilities;
using Shared.Data;
using UnityEngine;

namespace Client.VFX
{
    public class AbilityVfxRunner : Runner<AbilityVfx, AbilityRuntimeParams>
    {
        protected override AbilityVfx GetRunnable(ref AbilityRuntimeParams runtimeParams)
        {
            return AbilityVfx.CreateVfx(ref runtimeParams);
        }

        public override void AddRunnable(ref AbilityRuntimeParams runtimeParameter)
        {
            GameDataManager.TryGetAbilityDescriptionByType(runtimeParameter.AbilityType, out var description);
            if (description.isUnique &&
                CurrentRunnables.FirstOrDefault(r =>
                    r.Description.abilityType == description.abilityType) is var match && match != null)
            {
                if (!match.Reactivate())
                {
                    EndRunnable(match);
                }
            }
            else
            {
                base.AddRunnable(ref runtimeParameter);
            }
        }

        protected override bool UpdateRunnable(AbilityVfx runnable)
        {
            //TODO extract so expirable Runnable
            var core = base.UpdateRunnable(runnable);
            var canExpire = runnable.Description.duration > 0;
            var isExpired = canExpire && runnable.StartTime + runnable.Description.duration < Time.time;
            return core && !isExpired;
        }
    }
}