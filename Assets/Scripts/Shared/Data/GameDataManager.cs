using System.Collections.Generic;
using Shared.Abilities;
using UnityEngine;

namespace Shared.Data
{
    public static class GameDataManager
    {
        private static Dictionary<AbilityType, AbilityDescription> abilityTypeMap;


        public static bool TryGetAbilityDescriptionByType(AbilityType abilityType, out AbilityDescription abilityDescription)
        {
            if (abilityTypeMap == null)
            {
               InitializeTypeMap();
            }
            return abilityTypeMap.TryGetValue(abilityType, out abilityDescription);
        }

        private static void InitializeTypeMap()
        {
            abilityTypeMap = new Dictionary<AbilityType, AbilityDescription>();
            var abilityDescriptions = Resources.Load<AbilityResource>("AbilityResource")?.abilities;
            foreach (var description in abilityDescriptions)
            {
                abilityTypeMap[description.abilityType] = description;
            }
        }
    }
}