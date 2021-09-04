using Shared.Abilities;
using UnityEngine;

namespace Client.VFX
{
    public class AoeZoneVfx : AbilityVfx
    {
        public AoeZoneVfx(ref AbilityRuntimeParams abilityRuntimeParams, Transform effectTransform) : base(
            ref abilityRuntimeParams, effectTransform)
        {
        }

        public override bool Start()
        {
            var scaler = EffectTransform.GetComponent<VisualFXScaler>();
            if (scaler != null)
            {
                scaler.Scale(Description.size);
            }
            return true;
        }

        public override bool Update()
        {
            return true;
        }

        public override void Cancel()
        {
            throw new System.NotImplementedException();
        }
    }
}