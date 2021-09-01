using System;
using System.Collections.Generic;

namespace Shared.Abilities
{
    [Serializable]
    public class AbilityHitEffect
    {
        public TargetEffectType EffectType;
        public float? OverrideValue;
        public AbilityTargetType TargetType;
        public List<Condition> Conditions = new List<Condition>();
    }
}