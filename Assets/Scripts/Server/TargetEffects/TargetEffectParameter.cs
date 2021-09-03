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

        private TargetEffectParameter(ulong[] targets, ulong actor, Vector3 targetDirection, AbilityType abilityType,
            StatusEffectType statusEffectType)
        {
            Targets = targets;
            Actor = actor;
            TargetDirection = targetDirection;
            AbilityType = abilityType;
            StatusEffectType = statusEffectType;
        }

        public TargetEffectParameter(ulong[] targets, ulong actor, Vector3 targetDirection, AbilityType abilityType) :
            this(targets, actor, targetDirection, abilityType, StatusEffectType.None)
        {
        }

        public TargetEffectParameter(ulong[] targets, ulong actor, Vector3 targetDirection,
            StatusEffectType statusEffectType) : this(targets, actor, targetDirection, AbilityType.None,
            statusEffectType)
        {
        }
    }
}