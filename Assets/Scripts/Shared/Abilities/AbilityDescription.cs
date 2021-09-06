using UnityEngine;

namespace Shared.Abilities
{
    [CreateAssetMenu(menuName = "Ability")]
    public class AbilityDescription : CoreDescription
    {
        public AnimationCurve valueModifier;
        public AbilityType abilityType;
        public AbilityEffectType effect;
        public AbilityTargetType targetRequirement;
        public bool isUnique;
        public bool isInterruptable;
        public float cooldown;
        public int charges;
        public float speed;
        public float size;
        public float range;
        public float castTime;
        public bool rootsDuringCast;
        public bool isTracking;
        public GameObject[] Prefabs;
        public AbilityHitEffect[] HitEffects;
        public GameObject targetingPrefab;
    }
}