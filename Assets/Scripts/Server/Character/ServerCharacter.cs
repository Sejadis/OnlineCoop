using System;
using System.Collections.Generic;
using MLAPI;
using Server.Ability;
using Shared;
using Shared.Abilities;
using UnityEngine;

namespace Server
{
    [RequireComponent(typeof(NetworkCharacterState), typeof(ServerCharacterMovement), typeof(NetworkObject))]
    public class ServerCharacter : NetworkBehaviour, IDamagable, IHealable
    {
        protected AbilityHandler abilityHandler;
        protected NetworkCharacterState networkCharacterState;

        protected ServerCharacterMovement serverCharacterMovement;

        //TODO refactor into net state
        protected float moveSpeed = 5;
        protected float sprintSpeedMultiplier = 2;

        public NetworkCharacterState NetworkCharacterState => networkCharacterState;

        private void Awake()
        {
            abilityHandler = new AbilityHandler(this);
        }

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
        }

        protected virtual void Update()
        {
            abilityHandler.Update();
        }

        public void ForceMove(Vector3 targetPosition, float speed)
        {
            serverCharacterMovement.ForceMovement(targetPosition,speed);
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
                OnDeath?.Invoke(NetworkObjectId, actor);
            }
        }

        public event Action<ulong, ulong> OnDeath;

        public virtual void Heal(ulong actor, int amount)
        {
            var health = amount + networkCharacterState.NetHealthState.CurrentHealth.Value;
            health = Mathf.Clamp(health, 0, networkCharacterState.NetHealthState.MaxHealth.Value);
            networkCharacterState.NetHealthState.CurrentHealth.Value = health;
        }
    }
}