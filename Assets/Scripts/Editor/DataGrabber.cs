using Shared.Abilities;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [InitializeOnLoad]
    public static class DataGrabber
    {
        static DataGrabber()
        {
            var abilityResource = Resources.Load<AbilityResource>("AbilityResource");
            if (abilityResource == null)
            {
                var res = ScriptableObject.CreateInstance<AbilityResource>();
                AssetDatabase.CreateAsset(res, "Assets/Resources/AbilityResource.asset");
                AssetDatabase.SaveAssets();
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
    }
}