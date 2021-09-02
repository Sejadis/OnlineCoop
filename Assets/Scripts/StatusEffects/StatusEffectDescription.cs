﻿using Shared.Abilities;
using UnityEngine;

namespace StatusEffects
{
    [CreateAssetMenu(menuName = "StatusEffect")]
    public class StatusEffectDescription : CoreDescription
    {
        public StatusEffectType Type;
        public StatusEffectLogicType LogicType;
        public int maxStacks;

        public HitEffect[] HitEffects;
    }
}