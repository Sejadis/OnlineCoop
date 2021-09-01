using UnityEngine;

namespace Shared.Abilities
{
    [CreateAssetMenu(menuName = "Ability")]
    public class AbilityDescription : Description
    {
        public AbilityType abilityType;
        public AbilityEffectType effect;
        public bool isUnique;
        public bool isInterruptable;
        public float cooldown;
        public int charges;
        public float mainValue;
        public float speed;
        public float duration;
        public float size;
        public float range;
        public float delay;
        public float castTime;
        public float force;
        public bool rootsDuringCast;
        public bool isTracking;
        public AbilityDescription followUpAbility;
        public GameObject[] Prefabs;
        public AbilityHitEffect[] HitEffects;
        public GameObject targetingPrefab;
    }
}