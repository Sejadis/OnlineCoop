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

public class PlayerController : NetworkBehaviour
{
    [SerializeField] private float moveSpeed = 3;
    [SerializeField] private float sprintSpeedMultiplier = 2;
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private GameObject interactionUI;


    private Vector2 movementData;
    private IInteractable currentInteractable;

    private bool isSprinting;
    private CharacterNetworkState networkState;

    // Start is called before the first frame update
    public override void NetworkStart()
    {
        networkState = GetComponent<CharacterNetworkState>();

        if (!IsLocalPlayer)
        {
            playerCamera.SetActive(false);
            GameObject.FindWithTag("PartyUI").GetComponent<PartyUI>()
                .RegisterPartyMember("Player " + NetworkObjectId, networkState);
            return;
        }

        GameObject.FindWithTag("PartyUI").GetComponent<PartyUI>().RegisterPlayer("Player " + NetworkObjectId,
            networkState);
        var abilityUI = GameObject.FindWithTag("AbilityUI").GetComponent<AbilityUI>();
        if (GameDataManager.Instance.TryGetAbilityDescriptionByType(AbilityType.Fireball, out var ability1))
        {
            abilityUI.SetAbility(ability1, 0);
        }
        if(GameDataManager.Instance.TryGetAbilityDescriptionByType(AbilityType.ElectroPole, out var ability2))
        {
            abilityUI.SetAbility(ability2, 1);
        }
        if(GameDataManager.Instance.TryGetAbilityDescriptionByType(AbilityType.PoisonZone, out var ability3))
        {
            abilityUI.SetAbility(ability3, 2);
        }

        networkState.OnStartCooldown += abilityUI.TriggerCooldown;
        networkState.OnClientAbilityCast += OnClientAbilityCast;

        InputManager.Instance.OnMovement += OnMovement;
        InputManager.Instance.OnSprint += OnSprint;
        InputManager.Instance.OnLook += OnLook;

        InputManager.Instance.OnCore1 += OnCore1;
        InputManager.Instance.OnCore2 += OnCore2;
        InputManager.Instance.OnCore3 += OnCore3;
    }

    private void OnClientAbilityCast(AbilityRuntimeParams runtimeParams)
    {
        if(GameDataManager.Instance.TryGetAbilityDescriptionByType(runtimeParams.AbilityType, out var description))
        {
            var obj = Instantiate(description.Prefabs[0], runtimeParams.TargetPosition, Quaternion.identity);
            obj.transform.localScale = Vector3.one * description.range;
            Destroy(obj, description.duration > 0 ? description.duration : 1f);
        }
    }

    private void OnLook(InputAction.CallbackContext obj)
    {
        var value = obj.ReadValue<Vector2>();
        value.y *= -1;
        networkState.SendLookInputServerRpc(value);
    }

    private void OnCore1(InputAction.CallbackContext obj)
    {
        var runtimeParams = new AbilityRuntimeParams(AbilityType.Fireball, NetworkObjectId, 0, Vector3.zero,
            transform.forward, transform.position);
        networkState.CastAbilityServerRpc(runtimeParams);
    }

    private void OnCore2(InputAction.CallbackContext obj)
    {
        var runtimeParams = new AbilityRuntimeParams(AbilityType.LightningStrike, NetworkObjectId, 0,
            transform.position + transform.forward * 5, transform.forward, transform.position);
        networkState.CastAbilityServerRpc(runtimeParams);
    }

    private void OnCore3(InputAction.CallbackContext obj)
    {
        var runtimeParams = new AbilityRuntimeParams(AbilityType.PoisonZone, NetworkObjectId, 0,
            transform.position + transform.forward * 5, transform.forward, transform.position);
        networkState.CastAbilityServerRpc(runtimeParams);
    }


    private void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isSprinting = true;
            networkState.ToggleSprintServerRpc(true);
        }
        else if (context.canceled)
        {
            isSprinting = false;
            networkState.ToggleSprintServerRpc(false);
        }
    }

    private void OnMovement(InputAction.CallbackContext context)
    {
        var inputValue = context.ReadValue<Vector2>();
        movementData = inputValue.normalized;
        networkState.SendMoveInputServerRpc(movementData);
    }

    private void OnDestroy()
    {
        if (!IsOwner) return;
        InputManager.Instance.OnMovement -= OnMovement;
        InputManager.Instance.OnSprint -= OnSprint;
        InputManager.Instance.OnCore2 -= OnCore2;
        InputManager.Instance.OnCore1 -= OnCore1;

        // networkState.Position.OnValueChanged -= OnPositionChanged;
        // networkState.RotationY.OnValueChanged -= OnRotationChanged;
    }

    private void Update()
    {
        if (IsServer)
        {
            return;
            Debug.DrawLine(transform.position, transform.position + transform.forward * 5f, Color.red);
            if (Physics.Raycast(transform.position, transform.forward, out var hitInfo, 5f))
            {
                currentInteractable = hitInfo.transform.GetComponent<IInteractable>();
                var id = hitInfo.transform.GetComponent<NetworkObject>().NetworkObjectId;

                SetInteractionClientRpc(true, id);
            }
            else
            {
                currentInteractable = null;
                SetInteractionClientRpc(false, 0);
            }
        }
    }

    [ClientRpc]
    private void SetInteractionClientRpc(bool state, ulong id, ClientRpcParams rpcParams = default)
    {
        if (IsOwner)
        {
            interactionUI.SetActive(state);
            if (state)
            {
                currentInteractable = NetworkSpawnManager.SpawnedObjects[id].GetComponent<IInteractable>();
                InputManager.Instance.OnInteraction += OnInteraction;
            }
            else
            {
                currentInteractable = null;
                InputManager.Instance.OnInteraction -= OnInteraction;
            }
        }
    }

    private void OnInteraction(InputAction.CallbackContext context)
    {
        currentInteractable?.Interact();
    }
}

public interface IInteractable
{
    public void Interact();
}