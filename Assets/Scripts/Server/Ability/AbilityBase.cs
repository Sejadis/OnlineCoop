using System;
using Server.Ability.TargetEffects;
using Shared.Abilities;
using Shared.Data;
using UnityEngine;

namespace Server.Ability
{
    public abstract class AbilityBase : Runnable.Runnable
    {
        private readonly AbilityRuntimeParams abilityRuntimeParams;
        public AbilityRuntimeParams AbilityRuntimeParams => abilityRuntimeParams;
        private bool didCooldownStart = false;
        protected virtual bool CanStartCooldown { get; set; } = true;

        public AbilityDescription Description
        {
            get
            {
                //TODO cache the result
                if (GameDataManager.TryGetAbilityDescriptionByType(abilityRuntimeParams.AbilityType,
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

        public bool IsBlocking()
        {
            return Description.castTime > 0 && Time.time - StartTime < Description.castTime;
        }

        public bool ShouldStartCooldown()
        {
            var currentValue = didCooldownStart;
            didCooldownStart = didCooldownStart || CanStartCooldown;
            return !currentValue && CanStartCooldown;
        }

        public AbilityBase(ref AbilityRuntimeParams abilityRuntimeParams)
        {
            this.abilityRuntimeParams = abilityRuntimeParams;
        }
    }
}