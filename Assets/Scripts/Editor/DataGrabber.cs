using Shared.Abilities;
using Shared.Data;
using Shared.StatusEffects;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Editor
{
    [InitializeOnLoad]
    public class DataGrabber : IPreprocessBuildWithReport
    {
        static DataGrabber()
        {
            GrabAbilities();
            GrabStatusEffects();
            AssetDatabase.SaveAssets();
        }

        private static void GrabAbilities()
        {
            var abilityResource = Resources.Load<AbilityResource>("AbilityResource");
            if (abilityResource == null)
            {
                var res = ScriptableObject.CreateInstance<AbilityResource>();
                AssetDatabase.CreateAsset(res, "Assets/Resources/AbilityResource.asset");
                abilityResource = res;
            }

            var abilityDescriptions = AssetDatabase.FindAssets("t:AbilityDescription");
            abilityResource.abilities.Clear();
            foreach (var guid in abilityDescriptions)
            {
                abilityResource.abilities
                    .Add(AssetDatabase.LoadAssetAtPath<AbilityDescription>(AssetDatabase.GUIDToAssetPath(guid)));
            }
        }

        private static void GrabStatusEffects()
        {
            var statusEffectResource = Resources.Load<StatusEffectResource>("StatusEffectResource");
            if (statusEffectResource == null)
            {
                var res = ScriptableObject.CreateInstance<StatusEffectResource>();
                AssetDatabase.CreateAsset(res, "Assets/Resources/StatusEffectResource.asset");
                statusEffectResource = res;
            }

            var statusEffectDescriptions = AssetDatabase.FindAssets("t:StatusEffectDescription");
            statusEffectResource.statusEffects.Clear();
            foreach (var guid in statusEffectDescriptions)
            {
                statusEffectResource.statusEffects
                    .Add(AssetDatabase.LoadAssetAtPath<StatusEffectDescription>(AssetDatabase.GUIDToAssetPath(guid)));
            }
        }

        public int callbackOrder { get; }
        public void OnPreprocessBuild(BuildReport report)
        {
            GrabAbilities();
            GrabStatusEffects();
            AssetDatabase.SaveAssets();
        }
    }
}