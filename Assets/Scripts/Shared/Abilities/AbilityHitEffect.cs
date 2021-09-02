using System;

namespace Shared.Abilities
{
    [Serializable]
    public class AbilityHitEffect : HitEffect
    {
        public AbilityTargetType TargetType;
    }
}