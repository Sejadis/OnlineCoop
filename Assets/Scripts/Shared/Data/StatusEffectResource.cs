using System.Collections.Generic;
using Shared.StatusEffects;
using UnityEngine;

namespace Shared.Data
{
    public class StatusEffectResource : ScriptableObject
    {
        public List<StatusEffectDescription> statusEffects = new List<StatusEffectDescription>();
    }
}