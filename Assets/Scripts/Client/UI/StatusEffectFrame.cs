using System.Collections;
using Shared.Data;
using Shared.StatusEffects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Client.UI
{
    public class StatusEffectFrame : MonoBehaviour
    {
        [SerializeField] private Image iconImage;
        [SerializeField] private Image durationFillImage;
        [SerializeField] private TextMeshProUGUI durationText;

        public void Init(ref StatusEffectRuntimeParams runtimeParams)
        {
            GameDataManager.TryGetStatusEffectDescriptionByType(runtimeParams.EffectType, out var effect);
            iconImage.sprite = effect.icon;
            StartCoroutine(UpdateDuration(effect.duration));
        }

        private IEnumerator UpdateDuration(float duration)
        {
            var elapsedTime = 0f;
            var progress = 0f;
            while (progress <= 1)
            {
                progress = elapsedTime / duration;
                durationFillImage.fillAmount = progress;
                durationText.text = (duration - elapsedTime).ToString("0");
                yield return null;
                elapsedTime += Time.deltaTime;
            }

            Destroy(gameObject);
        }
    }
}