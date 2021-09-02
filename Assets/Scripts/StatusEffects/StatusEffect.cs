using System;
using Runnable;
using Server.Character;
using Shared.Data;

namespace StatusEffects
{
    public abstract class StatusEffect : Runnable.Runnable
    {
        protected ulong source;
        protected ServerCharacter target;
        protected StatusEffectType type;
        
        public StatusEffectDescription Description
        {
            get
            {
                //TODO cache the result
                if (GameDataManager.TryGetStatusEffectDescriptionByType(type,
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

        protected StatusEffect(ulong source, ServerCharacter target, StatusEffectType type)
        {
            this.source = source;
            this.target = target;
            this.type = type;
        }

        public static StatusEffect CreateStatusEffect(ref StatusEffectRuntimeParams runtimeParams, ServerCharacter serverCharacter)
        {
            return runtimeParams.EffectType switch
            {
                StatusEffectType.TouchOfLife => new OverTimeStatusEffect(runtimeParams.source, serverCharacter, runtimeParams.EffectType),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}