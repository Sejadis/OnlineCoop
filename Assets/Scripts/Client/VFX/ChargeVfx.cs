using Shared.Abilities;
using UnityEngine;

namespace Client.VFX
{
    public class ChargeVfx : AbilityVfx
    {
        private float duration;
        private float size;
        private float elapsedTime;

        public override bool Start()
        {
            duration = Description.duration;
            size = Description.size;
            return true;
        }

        public override bool Update()
        {
            var progress = elapsedTime / duration;
            EffectTransform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one * (size * 2), progress);
            elapsedTime += Time.deltaTime;
            return progress <= 1;
        }

        public override bool Reactivate()
        {
            return false; //leads to calling end
        }

        public override void Cancel()
        {
            throw new System.NotImplementedException();
        }

        public ChargeVfx(ref AbilityRuntimeParams abilityRuntimeParams, Transform effectTransform) : base(
            ref abilityRuntimeParams, effectTransform)
        {
        }
    }
}