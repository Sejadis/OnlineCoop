using System.Collections.Generic;
using UnityEngine;

namespace Shared.Abilities
{
    [CreateAssetMenu(menuName = "Ability Resource")]
    public class AbilityResource : ScriptableObject
    {
        public List<AbilityDescription> abilities = new List<AbilityDescription>();
    }
}