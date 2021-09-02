using System;
using System.Collections.Generic;
using Server.TargetEffects;
using Shared.StatusEffects;
using UnityEngine;

namespace Shared
{
    [Serializable]
    public class HitEffect
    {
        public TargetEffectType EffectType;
        public float? OverrideValue;
        public List<Condition> Conditions = new List<Condition>();
        public StatusEffectType StatusEffectType;//TODO probably move to AbilityHitEffect, can buffs create new buffs?
    }
}