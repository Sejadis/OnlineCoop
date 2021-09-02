using Shared.Abilities;
using StatusEffects;
using UnityEngine;

namespace Server.Ability.TargetEffects
{
    public struct TargetEffectParameter
    {
        public readonly ulong Target;
        public readonly ulong Actor;
        public readonly Vector3 TargetDirection;
        public readonly AbilityType AbilityType;
        public readonly StatusEffectType StatusEffectType;

        private TargetEffectParameter(ulong target, ulong actor, Vector3 targetDirection, AbilityType abilityType, StatusEffectType statusEffectType)
        {
            Target = target;
            Actor = actor;
            TargetDirection = targetDirection;
            AbilityType = abilityType;
            StatusEffectType = statusEffectType;
        }
        
        public TargetEffectParameter(ulong target, ulong actor, Vector3 targetDirection, AbilityType abilityType) : this(target,actor,targetDirection,abilityType, StatusEffectType.None)
        {
        }
        
        public TargetEffectParameter(ulong target, ulong actor, Vector3 targetDirection, StatusEffectType statusEffectType) : this(target,actor,targetDirection,AbilityType.None, statusEffectType)
        {
        }
    }
}