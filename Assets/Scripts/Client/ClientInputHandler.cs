using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using MLAPI;
using MLAPI.Messaging;
using MLAPI.Spawning;
using SejDev.Systems.Ability;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClientInputHandler : NetworkBehaviour
{
    [SerializeField] private float moveSpeed = 3;
    [SerializeField] private float sprintSpeedMultiplier = 2;
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private GameObject interactionUI;
    [SerializeField] private Transform aimTarget;

    private Vector2 movementData;

    private bool isSprinting;
    private NetworkCharacterState state;

    // Start is called before the first frame update
    public override void NetworkStart()
    {
        state = GetComponent<NetworkCharacterState>();

        if (!IsLocalPlayer)
        {
            playerCamera.SetActive(false);
            GameObject.FindWithTag("PartyUI").GetComponent<PartyUI>()
                .RegisterPartyMember("Player " + NetworkObjectId, state);
            return;
        }

        GameObject.FindWithTag("PartyUI").GetComponent<PartyUI>().RegisterPlayer("Player " + NetworkObjectId,
            state);
        var abilityUI = GameObject.FindWithTag("AbilityUI").GetComponent<AbilityUI>();
        if (GameDataManager.Instance.TryGetAbilityDescriptionByType(AbilityType.Fireball, out var ability1))
        {
            abilityUI.SetAbility(ability1, 0);
        }

        if (GameDataManager.Instance.TryGetAbilityDescriptionByType(AbilityType.ElectroPole, out var ability2))
        {
            abilityUI.SetAbility(ability2, 1);
        }

        if (GameDataManager.Instance.TryGetAbilityDescriptionByType(AbilityType.PoisonZone, out var ability3))
        {
            abilityUI.SetAbility(ability3, 2);
        }

        state.OnStartCooldown += abilityUI.TriggerCooldown;
        state.OnClientAbilityCast += OnClientAbilityCast;

        InputManager.Instance.OnMovement += OnMovement;
        InputManager.Instance.OnSprint += OnSprint;
        InputManager.Instance.OnLook += OnLook;

        InputManager.Instance.OnCore1 += OnCore1;
        InputManager.Instance.OnCore2 += OnCore2;
        InputManager.Instance.OnCore3 += OnCore3;
    }

    private void OnClientAbilityCast(AbilityRuntimeParams runtimeParams)
    {
        if (GameDataManager.Instance.TryGetAbilityDescriptionByType(runtimeParams.AbilityType, out var description))
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

            Destroy(obj, description.duration > 0 ? description.duration : 1f);
        }
    }

    private void OnLook(InputAction.CallbackContext obj)
    {
        var value = obj.ReadValue<Vector2>();
        value.y *= -1;
        state.SendLookInputServerRpc(value);
    }

    private void OnCore1(InputAction.CallbackContext obj)
    {
        var runtimeParams = new AbilityRuntimeParams(AbilityType.Fireball, NetworkObjectId, 0, Vector3.zero,
            aimTarget.forward, aimTarget.position);
        state.CastAbilityServerRpc(runtimeParams);
    }

    private void OnCore2(InputAction.CallbackContext obj)
    {
        var runtimeParams = new AbilityRuntimeParams(AbilityType.LightningStrike, NetworkObjectId, 0,
            transform.position + transform.forward * 5, aimTarget.forward, transform.position);
        state.CastAbilityServerRpc(runtimeParams);
    }

    private void OnCore3(InputAction.CallbackContext obj)
    {
        var runtimeParams = new AbilityRuntimeParams(AbilityType.PoisonZone, NetworkObjectId, 0,
            transform.position + transform.forward * 5, aimTarget.forward, transform.position);
        state.CastAbilityServerRpc(runtimeParams);
    }


    private void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isSprinting = true;
            state.ToggleSprintServerRpc(true);
        }
        else if (context.canceled)
        {
            isSprinting = false;
            state.ToggleSprintServerRpc(false);
        }
    }

    private void OnMovement(InputAction.CallbackContext context)
    {
        var inputValue = context.ReadValue<Vector2>();
        movementData = inputValue.normalized;
        state.SendMoveInputServerRpc(movementData);
    }

    private void OnDestroy()
    {
        if (!IsOwner) return;
        InputManager.Instance.OnMovement -= OnMovement;
        InputManager.Instance.OnSprint -= OnSprint;
        InputManager.Instance.OnCore2 -= OnCore2;
        InputManager.Instance.OnCore1 -= OnCore1;
    }
}
