using System.Collections;
using Shared.Abilities;
using Shared.Data;
using UnityEngine;

namespace Client.VFX
{
    public class ChargeVFX : VisualFX
    {
        private float duration;
        private float size;

        public override void Init(ref AbilityRuntimeParams runtimeParams)
        {
            if (GameDataManager.TryGetAbilityDescriptionByType(runtimeParams.AbilityType, out var description))
            {
                duration = description.duration;
                size = description.size;
                StartCoroutine(ChargeUp());
            }
        }

        private IEnumerator ChargeUp()
        {
            var elapsedTime = 0f;
            var progress = 0f;
            while (progress < 1)
            {
                progress = elapsedTime / duration;
                transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one * (size * 2), progress);
                yield return null;
                elapsedTime += Time.deltaTime;
            }
        }
    }
}