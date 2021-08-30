using UnityEngine;

namespace Shared.Abilities
{
    [CreateAssetMenu(menuName = "Ability")]
    public class AbilityDescription : ScriptableObject
    {
        public new string name;
        public string description;
        public Sprite icon;
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
        public AbilityEffectType[] HitEffects;
        public GameObject targetingPrefab;
    }
}

