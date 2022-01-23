using Client.VFX;
using Shared;
using Shared.Abilities;
using UnityEngine;

namespace Client.Character
{
    public class ClientVisualizer : MonoBehaviour
    {
        private AbilityVfxRunner vfxRunner;
        private NetworkCharacterState networkCharacterState;

        private void Start()
        {
            vfxRunner = new AbilityVfxRunner();
            networkCharacterState = GetComponent<NetworkCharacterState>();
            networkCharacterState.OnClientAbilityCast += OnClientAbilityCast;
        }

        private void Update()
        {
            vfxRunner.Update();
        }

        private void OnClientAbilityCast(AbilityRuntimeParams runtimeParams, bool asReactivation)
        {
            vfxRunner.AddRunnable(ref runtimeParams, asReactivation);
        }
    }
}