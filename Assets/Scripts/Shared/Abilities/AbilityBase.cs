using System;
using Shared.Data;

namespace Shared.Abilities
{
    public abstract class AbilityBase : Runnable.Runnable
    {
        private readonly AbilityRuntimeParams abilityRuntimeParams;

        protected AbilityBase(ref AbilityRuntimeParams abilityRuntimeParams)
        {
            this.abilityRuntimeParams = abilityRuntimeParams;
        }

        protected AbilityRuntimeParams AbilityRuntimeParams => abilityRuntimeParams;

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