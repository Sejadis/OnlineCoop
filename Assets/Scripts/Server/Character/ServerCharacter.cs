using System;
using Client.Character;
using MLAPI;
using Server.Ability;
using Server.StatusEffects;
using Shared;
using Shared.Abilities;
using Shared.StatusEffects;
using UnityEngine;

namespace Server.Character
{
    [RequireComponent(typeof(NetworkCharacterState),
        typeof(NetworkObject))]
    [RequireComponent(typeof(ClientVisualizer))] //cant have more than 3 components in the attribute
    public class ServerCharacter : NetworkBehaviour, IDamagable, IHealable, IBuffable
    {
        [SerializeField] private AbilityTargetType faction;
        [SerializeField] private bool isStaticCharacter;
        protected AbilityRunner AbilityRunner;
        protected StatusEffectRunner StatusEffectRunner;
        protected NetworkCharacterState networkCharacterState;

        protected ServerCharacterMovement serverCharacterMovement;

        //TODO refactor into net state
        protected float moveSpeed = 5;
        protected float sprintSpeedMultiplier = 2;

        public NetworkCharacterState NetworkCharacterState => networkCharacterState;
        public AbilityTargetType Faction => faction;

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
            serverCharacterMovement.OnMovementStarted += OnMovementStarted;
            networkCharacterState = GetComponent<NetworkCharacterState>();
            networkCharacterState.OnServerAbilityCast += OnAbilityCast;
        }

        private void OnMovementStarted()
        {
            AbilityRunner.Interrupt(InterruptType.Movement);
        }

        protected virtual void Update()
        {
            AbilityRunner.Update();
            StatusEffectRunner.Update();
        }

        public void ForceMove(Vector3 targetPosition, float speed)
        {
            if (!isStaticCharacter)
            {
                serverCharacterMovement.ForceMovement(targetPosition, speed);
            }
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

    public enum InterruptType
    {
        Movement
    }
}