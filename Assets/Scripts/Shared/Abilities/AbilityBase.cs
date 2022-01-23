using System;
using Shared.Data;

namespace Shared.Abilities
{
    public abstract class AbilityBase : Runnable.Runnable
    {
        protected AbilityBase(ref AbilityRuntimeParams abilityRuntimeParams)
        {
            AbilityRuntimeParams = abilityRuntimeParams;
        }

        public AbilityRuntimeParams AbilityRuntimeParams { get; }

        public AbilityDescription Description
        {
            get
            {
                //TODO cache the result
                if (GameDataManager.TryGetAbilityDescriptionByType(AbilityRuntimeParams.AbilityType,
                    out var result))
                {
                    return result;
                }
                else
                {
                    throw new ArgumentException("Unhandled AbilityType");
                }
            }
        }
    }
}