using Shared;
using UnityEngine;

namespace StatusEffect
{   
    [CreateAssetMenu(menuName = "StatusEffect")]
    public class StatusEffectDescription : Description
    {
        public StatusEffectType Type;
        public StatusEffectLogicType LogicType;
    }
}