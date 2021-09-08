using UnityEngine;

namespace Client.Ability
{
    public class OnReleaseCastHandler : CastHandler
    {
        private GameObject targetingObject;
        private AbilityInput abilityInput;

        public override void SetInputDown()
        {
            targetingObject = Object.Instantiate(AbilityDescription.targetingPrefab);
            abilityInput = targetingObject.GetComponent<AbilityInput>();
            abilityInput.Setup(AbilityDescription, BaseTransform);
        }

        public override void SetInputUp()
        {
            abilityInput.GetRuntimeParams(ref runtimeParams);
            ActivateAbility();
        }

        protected override void CleanUp()
        {
            base.CleanUp();
            Object.Destroy(targetingObject);
        }
    }
}