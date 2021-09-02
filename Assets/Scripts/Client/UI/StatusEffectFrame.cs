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
            var finish = Time.time + duration;
            var progress = 0f;
            do
            {
                var timeLeft = finish - Time.time;
                durationFillImage.fillAmount = progress;
                durationText.text = timeLeft.ToString("0");
                progress = 1 - timeLeft / duration;
                yield return null;
            } while (progress < 1);

            Destroy(gameObject);
        }
    }
}