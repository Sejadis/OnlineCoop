using System.Collections.Generic;
using Shared.Abilities;
using UnityEngine;

namespace Shared.Data
{
    public class GameDataManager : MonoBehaviour
    {
        [SerializeField] private AbilityDescription[] abilityDescriptions;

        private Dictionary<AbilityType, AbilityDescription> abilityTypeMap;



        public static GameDataManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogWarning("There is already a GameDataManager in the scene, deleting this");
                Destroy(this);
            }

            Instance = this;
        }

        public bool TryGetAbilityDescriptionByType(AbilityType abilityType, out AbilityDescription abilityDescription)
        {
            if (abilityTypeMap == null)
            {
               InitializeTypeMap();
            }
            return abilityTypeMap.TryGetValue(abilityType, out abilityDescription);
        }

        private void InitializeTypeMap()
        {
            abilityTypeMap = new Dictionary<AbilityType, AbilityDescription>();
            foreach (var description in abilityDescriptions)
            {
                abilityTypeMap[description.abilityType] = description;
            }
        }
    }
}