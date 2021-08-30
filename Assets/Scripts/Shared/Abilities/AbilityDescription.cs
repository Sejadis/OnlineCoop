using UnityEngine;

namespace Shared.Abilities
{
    [CreateAssetMenu(menuName = "Ability")]
    public class AbilityDescription : ScriptableObject
    {
        public string name;
        public string description;
        public Sprite icon;
        public AbilityType abilityType;
        public AbilityEffectType effect;
        public bool isUnique;
        public float cooldown;
        public int charges;
        public float mainValue;
        public float speed;
        public float duration;
        public float size;
        public float range;
        public float delay;
        public float castTime;
        public bool rootsDuringCast;
        public bool isTracking;
        public AbilityDescription followUpAbility;
        public GameObject[] Prefabs;
        public AbilityType[] HitEffects;
        public GameObject targetingPrefab;
    }
}

