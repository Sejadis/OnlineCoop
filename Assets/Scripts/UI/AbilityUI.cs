using System.Collections.Generic;
using System.Linq;
using SejDev.Systems.Ability;
using UnityEngine;

namespace UI
{
    public class AbilityUI : MonoBehaviour
    {
        [SerializeField] private AbilityFrame[] abilityFrames;

        public void SetAbility(AbilityDescription abilityDescription, int slot)
        {
            var frame = abilityFrames[slot];
            frame.SetIcon(abilityDescription.icon);
            frame.SetAbilityType(abilityDescription.abilityType);
        }

        public void TriggerCooldown(AbilityType abilityType, float cooldown)
        {
            var frame = abilityFrames.FirstOrDefault(frame => frame.AbilityType == abilityType);
            frame.SetCooldown(cooldown);
        }
    }
}