using System;

namespace Shared.Abilities
{
    [Serializable]
    public class AbilityHitEffect
    {
        public AbilityEffectType EffectType;
        public float? OverrideValue;
        public AbilityTargetType TargetType;
    }
}