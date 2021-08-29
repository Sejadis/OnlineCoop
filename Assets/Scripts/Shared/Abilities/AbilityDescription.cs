using UnityEngine;

namespace Shared.Abilities
{
    [CreateAssetMenu(menuName = "Ability")]
    public class AbilityDescription : ScriptableObject
    {
        public float cooldown;
        public int charges;
        public AbilityType abilityType;
        public AbilityEffectType effect;
        public float mainValue;
        public float speed;
        public float duration;
        public float size;
        public float range;
        public bool isTracking;
        public string name;
        public string description;
        public Sprite icon;
        public AbilityDescription followUpAbility;
        public GameObject[] Prefabs;
        public AbilityType[] HitEffects;
        public float delay;
        public GameObject targetingPrefab;
        public bool isUnique;
    }
}

