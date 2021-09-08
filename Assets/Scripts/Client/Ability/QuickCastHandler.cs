using UnityEngine;

namespace Client.Ability
{
    public class QuickCastHandler : CastHandler
    {
        public override void SetInputDown()
        {
            AbilityDescription.targetingPrefab.GetComponent<AbilityInput>()
                .GetRuntimeParams(ref runtimeParams, Camera.main, BaseTransform, AbilityDescription);
            ActivateAbility();
        }

        public override void SetInputUp()
        {
            throw new System.NotImplementedException();
        }
    }
}