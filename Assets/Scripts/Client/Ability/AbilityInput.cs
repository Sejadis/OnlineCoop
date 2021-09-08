using Shared.Abilities;
using UnityEngine;

namespace Client.Ability
{
    public abstract class AbilityInput : MonoBehaviour
    {
        protected AbilityDescription abilityDescription;
        protected Transform baseTransform;

        public void Setup(AbilityDescription abilityDescription, Transform baseTransform)
        {
            this.abilityDescription = abilityDescription;
            this.baseTransform = baseTransform;
        }

        public abstract void GetRuntimeParams(ref AbilityRuntimeParams runtimeParams);

        public abstract void GetRuntimeParams(ref AbilityRuntimeParams runtimeParams, Camera camera, Transform baseTransform,
            AbilityDescription abilityDescription);
    }
}