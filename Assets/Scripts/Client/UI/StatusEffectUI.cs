using Shared.StatusEffects;
using UnityEngine;

namespace Client.UI
{
    public class StatusEffectUI : UIScreen
    {
        [SerializeField] private Transform gridParent;
        [SerializeField] private GameObject effectPrefab;

        public void AddStatusEffect(ref StatusEffectRuntimeParams runtimeParams)
        {
            var obj = Instantiate(effectPrefab, gridParent, false);
            obj.GetComponent<StatusEffectFrame>().Init(ref runtimeParams);
        }
    }
}