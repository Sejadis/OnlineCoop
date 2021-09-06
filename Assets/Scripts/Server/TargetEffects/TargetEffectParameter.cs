using Shared.Abilities;
using Shared.StatusEffects;
using UnityEngine;

namespace Server.TargetEffects
{
    public struct TargetEffectParameter
    {
        public readonly ulong[] Targets;
        public readonly ulong Actor;
        public readonly Vector3 TargetDirection;
        public readonly AbilityType AbilityType;
        public readonly StatusEffectType StatusEffectType;
        public readonly float? OverrideValue;

        private TargetEffectParameter(ulong[] targets, ulong actor, Vector3 targetDirection, AbilityType abilityType,
            StatusEffectType statusEffectType, float? overrideValue)
        {
            Targets = targets;
            Actor = actor;
            TargetDirection = targetDirection;
            AbilityType = abilityType;
            StatusEffectType = statusEffectType;
            OverrideValue = overrideValue;
        }

        public TargetEffectParameter(
            ulong[] targets,
            ulong actor,
            Vector3 targetDirection,
            AbilityType abilityType,
            float? overrideValue = null)
            :
            this(targets, actor, targetDirection, abilityType, StatusEffectType.None, overrideValue)
        {
        }

        public TargetEffectParameter(
            ulong[] targets,
            ulong actor,
            Vector3 targetDirection,
            StatusEffectType statusEffectType,
            float? overrideValue = null)
            :
            this(targets, actor, targetDirection, AbilityType.None, statusEffectType, overrideValue)
        {
        }
    }
}