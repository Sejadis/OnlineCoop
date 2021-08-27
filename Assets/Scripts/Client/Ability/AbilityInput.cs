using System;
using Shared.Abilities;
using UnityEngine;

namespace Client.Ability
{
    public abstract class AbilityInput : MonoBehaviour
    {
        protected Action callback;
        protected AbilityDescription abilityDescription;
        protected Transform baseTransform;

        public void Setup(Action callback, AbilityDescription abilityDescription,  Transform baseTransform)
        {
            this.callback = callback;
            this.abilityDescription = abilityDescription;
            this.baseTransform = baseTransform;
        }

    }
}