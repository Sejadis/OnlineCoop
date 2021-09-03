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
        
        private void OnClientAbilityCast(AbilityRuntimeParams runtimeParams)
        {
            
            vfxRunner.AddRunnable(ref runtimeParams);
            
            // if (GameDataManager.TryGetAbilityDescriptionByType(runtimeParams.AbilityType, out var description))
            // {
            // var obj = Instantiate(description.Prefabs[0], runtimeParams.TargetPosition, Quaternion.identity);
            //     var scaler = obj.GetComponent<VisualFXScaler>();
            //     if (scaler != null)
            //     {
            //         scaler.Scale(description.size);
            //     }
            //     else
            //     {
            //         obj.transform.localScale = Vector3.one * description.size;
            //     }
            //     Destroy(obj, description.duration > 0 ? description.duration : 1f);
            // }
        }
    }
}