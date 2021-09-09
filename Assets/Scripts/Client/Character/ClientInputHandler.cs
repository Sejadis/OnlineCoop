using Client.Ability;
using Client.Input;
using Client.UI;
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
        [SerializeField] private SettingCastMode castMode;

        private NetworkCharacterState networkCharacterState;
        private CastHandler castHandler;

        public override void NetworkStart()
        {
            //do this always independent on local player state
            networkCharacterState = GetComponent<NetworkCharacterState>();

            if (!IsLocalPlayer)
            {
                playerCamera.SetActive(false);
                UIManager.Instance.GameHUD.InitPartyMember(networkCharacterState, "Player " + NetworkObjectId);
                return;
            }

            UIManager.Instance.GameHUD.InitPlayer(networkCharacterState, "Player " + NetworkObjectId);

            castHandler = CastHandler.CreateCastHandler(castMode.Value);
            castMode.OnValueChanged += (_, newMode) => castHandler = CastHandler.CreateCastHandler(newMode);

            InputManager.OnMovement += OnMovement;
            InputManager.OnSprint += OnSprint;
            InputManager.OnJump += OnJump;
            InputManager.OnLook += OnLook;

            InputManager.OnCore1 += OnCore1;
            InputManager.OnCore2 += OnCore2;
            InputManager.OnCore3 += OnCore3;
        }


        private void OnCore1(InputAction.CallbackContext context)
        {
            CastAbility(0, context);
        }

        private void OnCore2(InputAction.CallbackContext context)
        {
            CastAbility(1, context);
        }

        private void OnCore3(InputAction.CallbackContext context)
        {
            CastAbility(2, context);
        }

        private void CastAbility(int index, InputAction.CallbackContext context)
        {
            var abilityType = (AbilityType) networkCharacterState.equippedAbilities.Value[index];
            var runtimeParams = CreateRuntimeParams(abilityType);
            GameDataManager.TryGetAbilityDescriptionByType(abilityType, out var description);

            if (description.targetingPrefab == null)
            {
                if (context.started)
                {
                    networkCharacterState.CastAbilityServerRpc(runtimeParams);
                }
                //TODO make this conform to cast mode (make prefabs optional?)
                return;
            }

            //we have a targeting prefab, use it
            if (context.canceled && !castHandler.IsActive)
            {
                //ignore button up while no ability is being casted
                return;
            }

            if (context.started && !castHandler.IsActive)
            {
                //TODO check for cooldown or request start from server?
                castHandler.Start(description, aimTarget, ref runtimeParams,
                    (runtimeParams) => networkCharacterState.CastAbilityServerRpc(runtimeParams));
            }
            else
            {
                if (castHandler.CurrentAbility == abilityType)
                {
                    if (context.started)
                    {
                        castHandler.SetInputDown();
                    }
                    else if (context.canceled)
                    {
                        castHandler.SetInputUp();
                    }
                }
                else
                {
                    //TODO determine logic what happens when other ability is activated during active targeting
                    //cancel old ability or ignore new ability
                }
            }
        }

        private AbilityRuntimeParams CreateRuntimeParams(AbilityType abilityType)
        {
            var runtimeParams = new AbilityRuntimeParams(
                abilityType,
                NetworkObjectId,
                new ulong[0],
                transform.position + aimTarget.forward,
                aimTarget.forward,
                aimTarget.TransformPoint(localAbilitySpawnOffset));
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