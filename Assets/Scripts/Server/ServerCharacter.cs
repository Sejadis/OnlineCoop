using System.Collections;
using System.Collections.Generic;
using MLAPI;
using SejDev.Systems.Ability;
using UnityEngine;

[RequireComponent(typeof(NetworkCharacterState))]
public class ServerCharacter : NetworkBehaviour, IDamagable, IHealable
{
    protected AbilityHandler abilityHandler;
    protected NetworkCharacterState networkCharacterState;    
    protected Vector2 moveInput;
    //TODO refactor into net state
    protected float moveSpeed = 5;
    protected float sprintSpeedMultiplier = 2;
    
    // Start is called before the first frame update
    protected virtual void Start()
    {
        networkCharacterState = GetComponent<NetworkCharacterState>();
        abilityHandler = new AbilityHandler(networkCharacterState);
    }

    // Update is called once per frame
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

        networkCharacterState = GetComponent<NetworkCharacterState>();
        networkCharacterState.OnServerAbilityCast += OnAbilityCast;

        abilityHandler = new AbilityHandler(networkCharacterState);//TODO refactor to take a server char
    }

    protected virtual void Update()
    {
        abilityHandler.Update();
        ApplyMovement();
    }

    private void ApplyMovement()
    {
        var finalMove = moveInput.x * transform.right;
        finalMove += moveInput.y * transform.forward;
        finalMove.Normalize();
        finalMove *= networkCharacterState.IsSprinting.Value ? moveSpeed * sprintSpeedMultiplier : moveSpeed;
        finalMove *= Time.deltaTime;
        transform.position += finalMove;
    }
    protected void OnAbilityCast(AbilityRuntimeParams runtimeParams)
    {
        abilityHandler.StartAbility(ref runtimeParams);
    }

    public void Damage(int amount)
    {
        networkCharacterState.NetHealthState.CurrentHealth.Value -= amount;
    }

    public void Heal(int amount)
    {
        networkCharacterState.NetHealthState.CurrentHealth.Value += amount;
    }
}
