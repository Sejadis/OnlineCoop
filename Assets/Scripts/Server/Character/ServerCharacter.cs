using System;
using MLAPI;
using Server.Ability;
using Server.StatusEffects;
using Shared;
using Shared.Abilities;
using Shared.StatusEffects;
using UnityEngine;

namespace Server.Character
{
    [RequireComponent(typeof(NetworkCharacterState), typeof(ServerCharacterMovement), typeof(NetworkObject))]
    public class ServerCharacter : NetworkBehaviour, IDamagable, IHealable, IBuffable
    {
        protected AbilityRunner AbilityRunner;
        protected StatusEffectRunner StatusEffectRunner;
        protected NetworkCharacterState networkCharacterState;

        protected ServerCharacterMovement serverCharacterMovement;

        //TODO refactor into net state
        protected float moveSpeed = 5;
        protected float sprintSpeedMultiplier = 2;

        public NetworkCharacterState NetworkCharacterState => networkCharacterState;

        private void Awake()
        {
            AbilityRunner = new AbilityRunner(this);
            StatusEffectRunner = new StatusEffectRunner();
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
            AbilityRunner.Update();
            StatusEffectRunner.Update();
        }

        public void ForceMove(Vector3 targetPosition, float speed)
        {
            serverCharacterMovement.ForceMovement(targetPosition, speed);
        }

        protected void OnAbilityCast(AbilityRuntimeParams runtimeParams)
        {
            AbilityRunner.AddRunnable(ref runtimeParams);
        }

        public event Action<ulong, ulong> OnDeath;

        public virtual void Damage(ulong actor, int amount)
        {
            networkCharacterState.NetHealthState.CurrentHealth.Value -= amount;
            if (networkCharacterState.NetHealthState.CurrentHealth.Value <= 0)
            {
                OnDeath?.Invoke(NetworkObjectId, actor);
            }
        }

        public virtual void Heal(ulong actor, int amount)
        {
            var health = amount + networkCharacterState.NetHealthState.CurrentHealth.Value;
            health = Mathf.Clamp(health, 0, networkCharacterState.NetHealthState.MaxHealth.Value);
            networkCharacterState.NetHealthState.CurrentHealth.Value = health;
        }

        public void AddStatusEffect(ref StatusEffectRuntimeParams runtimeParams)
        {
            StatusEffectRunner.AddRunnable(ref runtimeParams);
        }
    }
}