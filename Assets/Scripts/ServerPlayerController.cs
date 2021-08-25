using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Prototyping;
using SejDev.Systems.Ability;
using UnityEngine.InputSystem;

public class ServerPlayerController : NetworkBehaviour, IDamagable, IHealable
{
    [SerializeField] private Transform followTarget;
    private CharacterNetworkState networkState;
    private AbilityHandler abilityHandler;

    public float moveSpeed = 5;

    public float sprintSpeedMultiplier = 2;

    public Vector2 moveInput;
    public Vector2 lookInput;
    public float sensitivity = 1f;
    public float yRotation;
    public float xRotation;

    // Start is called before the first frame update
    public override void NetworkStart()
    {
        if (!IsServer)
        {
            enabled = false;
            return;
        }

        if (!IsOwner)
        {
        }

        networkState = GetComponent<CharacterNetworkState>();
        networkState.OnMoveInputReceived += OnMoveInputReceived;
        networkState.OnLookInputReceived += OnLookInputReceived;
        networkState.OnSprintReceived += OnSprintReceived;
        networkState.OnAbilityCast += OnAbilityCast;

        abilityHandler = new AbilityHandler(networkState);

        yRotation = transform.rotation.eulerAngles.y;
        xRotation = followTarget.rotation.eulerAngles.x;
    }

    private void OnAbilityCast(AbilityRuntimeParams runtimeParams)
    {
        abilityHandler.StartAbility(ref runtimeParams);
    }

    private void OnSprintReceived(bool sprint)
    {
        networkState.IsSprinting.Value = sprint;
    }

    private void OnMoveInputReceived(Vector2 input)
    {
        moveInput = input;
    }

    private void OnLookInputReceived(Vector2 input)
    {
        lookInput = input;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner)
        {
            //give ownership to server (which i think is always 0?)
            GetComponent<NetworkObject>().ChangeOwnership(0);
        }
        ApplyMovement();

        yRotation += lookInput.x * sensitivity;
        yRotation %= 360; //keep the number small

        transform.rotation =
            Quaternion.Euler(transform.rotation.eulerAngles.x, yRotation, transform.rotation.eulerAngles.z);
        
        xRotation += lookInput.y * sensitivity;
        xRotation = Mathf.Clamp(xRotation, -90f, 70f);
        followTarget.rotation = Quaternion.Euler(xRotation, followTarget.rotation.eulerAngles.y,
            followTarget.rotation.eulerAngles.z);
        
        abilityHandler.Update();
    }

    private void ApplyMovement()
    {
        var finalMove = moveInput.x * transform.right;
        finalMove += moveInput.y * transform.forward;
        finalMove.Normalize();
        finalMove *= networkState.IsSprinting.Value ? moveSpeed * sprintSpeedMultiplier : moveSpeed;
        finalMove *= Time.deltaTime;
        transform.position += finalMove;
    }

    public void Damage(int amount)
    {
        networkState.NetHealthState.CurrentHealth.Value -= amount;
    }

    public void Heal(int amount)
    {
        networkState.NetHealthState.CurrentHealth.Value += amount;
    }
}