using UnityEngine;

namespace Client.Ability
{
    public class NormalCastHandler : CastHandler
    {
        private GameObject targetingObject;
        private bool inputWasReleased;
        private AbilityInput abilityInput;

        public override void SetInputDown()
        {
            if (!inputWasReleased)
            {
                //initial key press, spawn the targeting prefab and let it do its job
                targetingObject = Object.Instantiate(AbilityDescription.targetingPrefab);
                abilityInput = targetingObject.GetComponent<AbilityInput>();
                abilityInput.Setup(AbilityDescription, BaseTransform);
            }
        }

        public override void SetInputUp()
        {
            if (!inputWasReleased)
            {
                inputWasReleased = true;
            }
            else
            {
                abilityInput.GetRuntimeParams(ref runtimeParams);
                ActivateAbility();
            }
        }

        protected override void CleanUp()
        {
            base.CleanUp();
            inputWasReleased = false;
            Object.Destroy(targetingObject);
        }
    }
}