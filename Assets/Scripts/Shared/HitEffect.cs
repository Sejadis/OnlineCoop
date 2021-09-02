using System.Collections.Generic;
using Server.Ability.TargetEffects;

namespace Shared.Abilities
{
    public class HitEffect
    {
        public TargetEffectType EffectType;
        public float? OverrideValue;
        public List<Condition> Conditions = new List<Condition>();
    }
}