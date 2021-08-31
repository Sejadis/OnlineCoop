using System;
using MLAPI;
using MLAPI.Spawning;
using Shared;
using Shared.Abilities;
using Shared.Data;
using UnityEngine;

namespace Server.Ability
{
    public abstract class Ability
    {
        public float StartTime { get; set; } = -1f;
        public bool IsStarted => StartTime > 0;
        private readonly AbilityRuntimeParams abilityRuntimeParams;
        public AbilityRuntimeParams AbilityRuntimeParams => abilityRuntimeParams;
        protected bool CanStartCooldown { get; set; } = true;
        protected bool DidCastTimePass => Description.castTime == 0 || Time.time - StartTime > Description.castTime;
        private bool didCooldownStart = false;

        public Ability(ref AbilityRuntimeParams abilityRuntimeParams)
        {
            this.abilityRuntimeParams = abilityRuntimeParams;
        }

        public AbilityDescription Description
        {
            get
            {
                //TODO cache the result
                if (GameDataManager.TryGetAbilityDescriptionByType(abilityRuntimeParams.AbilityType,
                    out var result))
                {
                    return result;
                }
                else
                {
                    throw new ArgumentException("Unhandled AbilityType");
                }
            }
        }

        public virtual bool Reactivate()
        {
            throw new NotSupportedException(
                $"Can not call Ability.Reactivate() on an ability that doesnt support it ({Description.abilityType})");
        }

        public bool ShouldStartCooldown()
        {
            var currentValue = didCooldownStart;
            didCooldownStart = didCooldownStart || CanStartCooldown;
            return abilityRuntimeParams.EffectType == AbilityEffectType.None && !currentValue && CanStartCooldown;
        }

        public abstract bool Start();

        public abstract bool Update();

        public virtual void End(){}

        public bool IsBlocking()
        {
            return Description.castTime > 0 && Time.time - StartTime < Description.castTime;
        }

        //TODO probably take better parameter than collider
        public virtual void RunHitEffects(Collider other, Vector3 targetPosition, Vector3 targetDirection, Vector3 startPosition, Transform abilityObject = null)
        {
            //TODO refactor to decrease duplicated code (like server aoe zone)
//TODO catch self (target mask)
            var netObject = other.GetComponent<NetworkObject>();
            if (other.gameObject.name == "PlayerPrefab(Clone)" || netObject == null)
            {
                return;
            }

            var actor = NetworkSpawnManager.SpawnedObjects[abilityRuntimeParams.Actor].GetComponent<NetworkCharacterState>();
            foreach (var hitEffect in Description.HitEffect2)
            {
                var runtimeParams = new AbilityRuntimeParams(
                    abilityType: Description.abilityType, 
                    actor: abilityRuntimeParams.Actor,
                    targetEntity:netObject.NetworkObjectId,
                    targetPosition:targetPosition,
                    targetDirection:targetDirection, 
                    startPosition:startPosition, 
                    effectType:hitEffect.EffectType);
                
                if (hitEffect.TargetType != AbilityTargetType.None)
                {
                    runtimeParams.TargetEntity = hitEffect.TargetType switch
                    {
                        AbilityTargetType.Self => abilityObject?.GetComponent<NetworkObject>()?.NetworkObjectId
                                                  ?? throw new InvalidOperationException("Can not target self on an ability without NetworkObject component"),
                        AbilityTargetType.Actor => abilityRuntimeParams.Actor,
                        _ => throw new ArgumentOutOfRangeException()
                    };
                }

                actor.CastAbilityServerRpc(runtimeParams);
            }
        }

        public static Ability CreateAbility(ref AbilityRuntimeParams runtimeParams)
        {
            var effectType = runtimeParams.EffectType;
            if (effectType == AbilityEffectType.None){
                if (GameDataManager.TryGetAbilityDescriptionByType(
                    runtimeParams.AbilityType, out var abilityDescription))
                {
                    effectType = abilityDescription.effect;
                }
                else
                {
                    throw new ArgumentException("Unhandled AbilityType");
                }
            }

            return GetAbilityByEffectType(effectType, ref runtimeParams);
        }

        private static Ability GetAbilityByEffectType(AbilityEffectType effectType,
            ref AbilityRuntimeParams runtimeParams)
        {
            return effectType switch
            {
                AbilityEffectType.Projectile => new ProjectileAbility(ref runtimeParams),
                AbilityEffectType.Craft => new CraftItemAbility(ref runtimeParams),
                AbilityEffectType.AoeZone => new AoeZoneAbility(ref runtimeParams),
                AbilityEffectType.AoeOneShot => new AoeAbility(ref runtimeParams),
                AbilityEffectType.Log => new LogAbility(ref runtimeParams),
                AbilityEffectType.SpawnObject => new SpawnObjectAbility(ref runtimeParams),
                AbilityEffectType.Damage => new DamageAbility(ref runtimeParams),
                AbilityEffectType.ChargeAoeOneShot => new ChargedAoeAbility(ref runtimeParams),
                AbilityEffectType.Destroy => new DestroyAbility(ref runtimeParams),
                AbilityEffectType.Heal => new HealAbility(ref runtimeParams),
                AbilityEffectType.ForceMove => new ForceMoveAbility(ref runtimeParams),
                _ => throw new Exception("Unhandled AbilityEffectType"),
            };
        }
    }
}