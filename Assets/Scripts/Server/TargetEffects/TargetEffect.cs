using System;
using Shared;
using Shared.Abilities;
using Shared.Data;
using Shared.StatusEffects;

namespace Server.TargetEffects
{
    public abstract class TargetEffect
    {
        private TargetEffectParameter effectParameter;
        protected TargetEffectParameter EffectParameter => effectParameter;

        protected TargetEffect(TargetEffectParameter targetEffectParameter)
        {
            this.effectParameter = targetEffectParameter;
        }

        public abstract void Run();

        public static TargetEffect GetEffectByType(TargetEffectType effectType,
            TargetEffectParameter runtimeParams)
        {
            return effectType switch
            {
                TargetEffectType.Damage => new DamageAbility(runtimeParams),
                TargetEffectType.Destroy => new DestroyAbility(runtimeParams),
                TargetEffectType.Heal => new HealAbility(runtimeParams),
                TargetEffectType.ForceMove => new ForceMoveAbility(runtimeParams),
                TargetEffectType.Buff => new StatusEffectAbility(runtimeParams),
            _ => throw new Exception("Unhandled TargetEffectType"),
            };
        }

        protected CoreDescription SourceDescription
        {
            get
            {
                if (effectParameter.AbilityType != AbilityType.None &&
                    effectParameter.StatusEffectType != StatusEffectType.None)
                {
                    throw new ArgumentException(
                        "TargetEffect can not execute for status effect and ability at the same time");
                }

                if (effectParameter.AbilityType != AbilityType.None)
                {
                    GameDataManager.TryGetAbilityDescriptionByType(effectParameter.AbilityType, out var description);
                    return description;
                }
                else if (effectParameter.StatusEffectType != StatusEffectType.None)
                {
                    GameDataManager.TryGetStatusEffectDescriptionByType(effectParameter.StatusEffectType,
                        out var description);
                    return description;
                }
                else
                {
                    throw new ArgumentException("TargetEffect called without source set");
                }
            }
        }
    }
}