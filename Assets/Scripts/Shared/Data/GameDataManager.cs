using System.Collections.Generic;
using Shared.Abilities;
using StatusEffects;
using UnityEngine;

namespace Shared.Data
{
    public static class GameDataManager
    {
        private static Dictionary<AbilityType, AbilityDescription> abilityTypeMap;
        private static Dictionary<StatusEffectType, StatusEffectDescription> statusEffectTypeMap;


        public static bool TryGetAbilityDescriptionByType(AbilityType abilityType,
            out AbilityDescription abilityDescription)
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

            statusEffectTypeMap = new Dictionary<StatusEffectType, StatusEffectDescription>();
            var statusEffectDescriptions = Resources.Load<StatusEffectResource>("StatusEffectResource")?.statusEffects;
            foreach (var description in statusEffectDescriptions)
            {
                statusEffectTypeMap[description.Type] = description;
            }
        }

        public static bool TryGetStatusEffectDescriptionByType(StatusEffectType statusEffectType,
            out StatusEffectDescription statusEffectDescription)
        {
            if (statusEffectTypeMap == null)
            {
                InitializeTypeMap();
            }

            return statusEffectTypeMap.TryGetValue(statusEffectType, out statusEffectDescription);
        }
    }

    public class StatusEffectResource : ScriptableObject
    {
        public List<StatusEffectDescription> statusEffects = new List<StatusEffectDescription>();
    }
}