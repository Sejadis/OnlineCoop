using System;
using System.Collections.Generic;
using MLAPI;
using Server.Ability;
using Shared;
using Shared.Abilities;
using UnityEngine;

namespace Server
{
    [RequireComponent(typeof(NetworkCharacterState), typeof(ServerCharacterMovement))]
    public class ServerCharacter : NetworkBehaviour, IDamagable, IHealable
    {
        protected AbilityHandler abilityHandler;
        protected NetworkCharacterState networkCharacterState;

        protected ServerCharacterMovement serverCharacterMovement;

        //TODO refactor into net state
        protected float moveSpeed = 5;
        protected float sprintSpeedMultiplier = 2;

        public NetworkCharacterState NetworkCharacterState => networkCharacterState;

        // Update is called once per frame
        public override void NetworkStart()
        {
            if (!IsServer)
            {
                enabled = false;
                return;
            }
            serverCharacterMovement = GetComponent<ServerCharacterMovement>();
            networkCharacterState = GetComponent<NetworkCharacterState>();
            networkCharacterState.OnServerAbilityCast += OnAbilityCast;

            abilityHandler = new AbilityHandler(this);
        }

        protected virtual void Update()
        {
            abilityHandler.Update();
        }

        protected void OnAbilityCast(AbilityRuntimeParams runtimeParams)
        {
            abilityHandler.StartAbility(ref runtimeParams);
        }

        public virtual void Damage(ulong actor, int amount)
        {
            networkCharacterState.NetHealthState.CurrentHealth.Value -= amount;
            if (networkCharacterState.NetHealthState.CurrentHealth.Value <= 0)
            {
                OnDeath?.Invoke(NetworkObjectId,actor);
            }
        }

        public event Action<ulong, ulong> OnDeath;

        public virtual void Heal(int amount)
        {
            networkCharacterState.NetHealthState.CurrentHealth.Value += amount;
        }
    }
}