using System;
using Client.Input;
using Client.UI;
using Client.VFX;
using Shared;
using Shared.Data;
using Shared.Settings;
using Shared.Abilities;
using MLAPI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Client.Character
{
    public class ClientInputHandler : NetworkBehaviour
    {
        [SerializeField] private GameObject playerCamera;
        [SerializeField] private Transform aimTarget;
        [SerializeField] private Vector3 localAbilitySpawnOffset;
        [SerializeField] private SettingBool invertMouseSetting;

        private NetworkCharacterState networkCharacterState;

        public override void NetworkStart()
        {
            //do this always in dependent on local player state
            networkCharacterState = GetComponent<NetworkCharacterState>();
            networkCharacterState.OnClientAbilityCast += OnClientAbilityCast;

            if (!IsLocalPlayer)
            {
                playerCamera.SetActive(false);
                GameObject.FindWithTag("PartyUI").GetComponent<PartyUI>()
                    .RegisterPartyMember("Player " + NetworkObjectId, networkCharacterState);
                return;
            }
            
            GameObject.FindWithTag("PartyUI").GetComponent<PartyUI>().RegisterPlayer("Player " + NetworkObjectId,
                networkCharacterState);
            var abilityUI = GameObject.FindWithTag("AbilityUI").GetComponent<AbilityUI>();

            for (int i = 0; i < 3; i++)
            {
                var type = (AbilityType) networkCharacterState.equippedAbilities.Value[i];
                if (GameDataManager.TryGetAbilityDescriptionByType(type, out var ability))
                {
                    abilityUI.SetAbility(ability, i);
                }
            }

            networkCharacterState.OnStartCooldown += abilityUI.TriggerCooldown;

            var statusEffectUI = GameObject.FindWithTag("StatusEffectUI").GetComponent<StatusEffectUI>();

            networkCharacterState.OnClientStatusEffectAdded +=
                runtimeParams => statusEffectUI.AddStatusEffect(ref runtimeParams);

            InputManager.OnMovement += OnMovement;
            InputManager.OnSprint += OnSprint;
            InputManager.OnJump += OnJump;
            InputManager.OnLook += OnLook;

            InputManager.OnCore1 += OnCore1;
            InputManager.OnCore2 += OnCore2;
            InputManager.OnCore3 += OnCore3;
        }


        private void OnClientAbilityCast(AbilityRuntimeParams runtimeParams)
        {
            if (GameDataManager.TryGetAbilityDescriptionByType(runtimeParams.AbilityType, out var description))
            {
                var obj = Instantiate(description.Prefabs[0], runtimeParams.TargetPosition, Quaternion.identity);
                var scaler = obj.GetComponent<VisualFXScaler>();
                if (scaler != null)
                {
                    scaler.Scale(description.size);
                }
                else
                {
                    obj.transform.localScale = Vector3.one * description.size;
                }

                obj.GetComponent<VisualFX>()?.Init(ref runtimeParams);

                Destroy(obj, description.duration > 0 ? description.duration : 1f);
            }
        }

        private void OnCore1(InputAction.CallbackContext obj)
        {
            CastAbility(0);
        }

        private void OnCore2(InputAction.CallbackContext obj)
        {
            CastAbility(1);
        }

        private void OnCore3(InputAction.CallbackContext obj)
        {
            CastAbility(2);
        }

        private void CastAbility(int index)
        {
            var abilityType = (AbilityType) networkCharacterState.equippedAbilities.Value[index];
            var runtimeParams = CreateRuntimeParams(abilityType);
            networkCharacterState.CastAbilityServerRpc(runtimeParams);
        }

        private AbilityRuntimeParams CreateRuntimeParams(AbilityType abilityType)
        {
            var runtimeParams = new AbilityRuntimeParams(abilityType, NetworkObjectId, 0,
                transform.position + aimTarget.forward,
                aimTarget.forward, aimTarget.TransformPoint(localAbilitySpawnOffset));
            return runtimeParams;
        }


        private void OnSprint(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                networkCharacterState.ToggleSprintServerRpc(true);
            }
            else if (context.canceled)
            {
                networkCharacterState.ToggleSprintServerRpc(false);
            }
        }

        private void OnMovement(InputAction.CallbackContext context)
        {
            var inputValue = context.ReadValue<Vector2>();
            networkCharacterState.SendMoveInputServerRpc(inputValue.normalized);
        }

        private void OnJump(InputAction.CallbackContext obj)
        {
            networkCharacterState.SetJumpServerRpc();
        }

        private void OnLook(InputAction.CallbackContext obj)
        {
            var value = obj.ReadValue<Vector2>();
            if (invertMouseSetting.Value)
            {
                value.y *= -1;
            }

            networkCharacterState.SendLookInputServerRpc(Utility.GetRelativeMouseDelta(value));
        }

        private void OnDestroy()
        {
            if (!IsOwner) return;
            InputManager.OnMovement -= OnMovement;
            InputManager.OnSprint -= OnSprint;
            InputManager.OnJump -= OnJump;
            InputManager.OnLook -= OnLook;

            InputManager.OnCore1 -= OnCore1;
            InputManager.OnCore2 -= OnCore2;
            InputManager.OnCore3 -= OnCore3;
        }
    }
}