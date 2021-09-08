using System;
using Shared.Abilities;
using UnityEngine;

namespace Client.Ability
{
    public abstract class CastHandler
    {
        private AbilityDescription abilityDescription;
        private Action<AbilityRuntimeParams> callback;
        private bool didActivate;
        private Transform baseTransform;
        protected AbilityRuntimeParams runtimeParams;

        protected ref AbilityRuntimeParams RuntimeParams => ref runtimeParams;

        protected Transform BaseTransform => baseTransform;

        protected AbilityDescription AbilityDescription => abilityDescription;
        public AbilityType CurrentAbility => abilityDescription.abilityType;
        public bool IsActive { get; private set; }

        public abstract void SetInputDown();
        public abstract void SetInputUp();

        protected virtual void CleanUp()
        {
            abilityDescription = null;
            runtimeParams = default;
            baseTransform = null;
            callback = null;
            IsActive = false;
        }

        public void Start(AbilityDescription abilityDescription, Transform baseTransform,
            ref AbilityRuntimeParams abilityRuntimeParams, Action<AbilityRuntimeParams> activationCallback)
        {
            this.runtimeParams = abilityRuntimeParams;
            this.baseTransform = baseTransform;
            this.abilityDescription = abilityDescription;
            callback = activationCallback;
            didActivate = false;
            IsActive = true;
            SetInputDown();
        }

        protected void ActivateAbility()
        {
            if (didActivate)
            {
                throw new InvalidOperationException(
                    "Can not activate CastHandler multiple times without calling Start");
            }

            didActivate = true;
            callback(runtimeParams);
            CleanUp();
        }

        public static CastHandler CreateCastHandler(AbilityCastMode castMode)
        {
            return castMode switch
            {
                AbilityCastMode.Normal => new NormalCastHandler(),
                AbilityCastMode.Quick => new QuickCastHandler(),
                AbilityCastMode.OnRelease => new OnReleaseCastHandler(),
                _ => throw new ArgumentException("Unhandled AbilityCastMode when creating CastHandler")
            };
        }
    }
}