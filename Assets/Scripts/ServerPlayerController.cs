using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Prototyping;
using SejDev.Systems.Ability;
using UnityEngine.InputSystem;

public class ServerPlayerController : NetworkBehaviour, IDamagable, IHealable
{

    private NetworkState networkState;
    private AbilityHandler abilityHandler;

    public float moveSpeed = 5;

    public float sprintSpeedMultiplier = 2;

    public Vector2 input;
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
        
        networkState = GetComponent<NetworkState>();
        networkState.OnInputReceived += OnInputReceived;
        networkState.OnSprintReceived += OnSprintReceived;
        networkState.OnAbilityCast += OnAbilityCast;
        
        abilityHandler = new AbilityHandler(networkState);
    }

    private void OnAbilityCast(AbilityRuntimeParams runtimeParams)
    {
        abilityHandler.StartAbility(ref runtimeParams);
    }

    private void OnSprintReceived(bool sprint)
    {
        networkState.IsSprinting.Value = sprint;
    }

    private void OnInputReceived(Vector2 input)
    {
        this.input = input;
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

        abilityHandler.Update();
    }

    private void ApplyMovement()
    {
        var finalMove = input.x * transform.right;
        finalMove += input.y * transform.forward;
        finalMove.Normalize();
        finalMove *= networkState.IsSprinting.Value ? moveSpeed * sprintSpeedMultiplier : moveSpeed;
        finalMove *= Time.deltaTime;
        transform.position += finalMove;
    }

    public void Damage(int amount)
    {
        networkState.CurrentHealth.Value -= amount;
    }

    public void Heal(int amount)
    {
        networkState.CurrentHealth.Value += amount;
    }
}
