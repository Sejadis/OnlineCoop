using Shared;
using Shared.Abilities;
using Shared.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Client.UI
{
    public class AbilityProgressUI : UIScreen
    {
        [SerializeField] private Image progressImage;
        [SerializeField] private GameObject container;
        private float elapsedTime;
        private float duration;
        private bool isCasting;

        public void Init(NetworkCharacterState networkCharacterState)
        {
            networkCharacterState.OnClientAbilityCast += OnClientAbilityCast;
            networkCharacterState.OnClientAbiltyCastCanceled += OnAbilityCastCanceled;
        }

        private void OnAbilityCastCanceled()
        {
            isCasting = false;
            container.SetActive(false);
            //TODO some visual indication for cancel
        }

        private void OnClientAbilityCast(AbilityRuntimeParams runtimeParams)
        {
            if (GameDataManager.TryGetAbilityDescriptionByType(runtimeParams.AbilityType, out var description))
            {
                duration = description.castTime;
                elapsedTime = 0;
                isCasting = true;
                progressImage.fillAmount = 0f;
                container.SetActive(true);
            }
        }

        private void Update()
        {
            if (!isCasting) return;
            elapsedTime += Time.deltaTime;
            var progress = elapsedTime / duration;
            progressImage.fillAmount = progress;
            if (progress > 1)
            {
                isCasting = false;
                container.SetActive(false);
            }
        }
    }
}