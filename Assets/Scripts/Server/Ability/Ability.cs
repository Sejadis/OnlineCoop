using System;
using MLAPI;
using MLAPI.Spawning;
using Server.TargetEffects;
using Shared;
using Shared.Abilities;
using Shared.Data;
using UnityEngine;

namespace Server.Ability
{
    public abstract class Ability : AbilityBase
    {
        protected bool DidCastTimePass => Description.castTime == 0 || Time.time - StartTime >= Description.castTime;
        private int hitCount = 0;

        public Ability(ref AbilityRuntimeParams abilityRuntimeParams) : base(ref abilityRuntimeParams)
        {
        }


        public virtual bool Reactivate()
        {
            throw new NotSupportedException(
                $"Can not call Ability.Reactivate() on an ability that doesnt support it ({Description.abilityType})");
        }

        public override void End()
        {
        }

        public override void Cancel()
        {
            CanStartCooldown = false;
        }


        //TODO probably take better parameter than collider
        public virtual void RunHitEffects(Collider other, Vector3 targetPosition, Vector3 targetDirection,
            Vector3 startPosition, Transform abilityObject = null)
        {
            //TODO refactor to decrease duplicated code (like server aoe zone)
//TODO catch self (target mask)
            var netObject = other.GetComponent<NetworkObject>();
            if (other.gameObject.name == "PlayerPrefab(Clone)" || netObject == null)
            {
                return;
            }

            hitCount++;

            var actor = NetworkSpawnManager.SpawnedObjects[AbilityRuntimeParams.Actor]
                .GetComponent<NetworkCharacterState>();
            foreach (var hitEffect in Description.HitEffects)
            {
                var conditionMet = true;
                foreach (var condition in hitEffect.Conditions)
                {
                    conditionMet = conditionMet && condition.Evaluate(hitCount);
                }

                if (!conditionMet)
                {
                    continue;
                }

                var target = hitEffect.TargetType switch
                {
                    AbilityTargetType.None => netObject.NetworkObjectId,
                    AbilityTargetType.Self => abilityObject?.GetComponent<NetworkObject>()?.NetworkObjectId
                                              ?? throw new InvalidOperationException(
                                                  "Can not target self on an ability without NetworkObject component"),
                    AbilityTargetType.Actor => AbilityRuntimeParams.Actor,
                    _ => throw new ArgumentOutOfRangeException()
                };

                //TODO refactor to move logic outside
                var runtimeParams = hitEffect.EffectType == TargetEffectType.Buff
                    ? new TargetEffectParameter(
                        target: target,
                        actor: AbilityRuntimeParams.Actor,
                        targetDirection: targetDirection,
                        statusEffectType: hitEffect.StatusEffectType
                        // targetPosition: targetPosition,
                        // startPosition: startPosition,
                        // effectType: hitEffect.EffectType,
                    )
                    : new TargetEffectParameter(
                        target: target,
                        actor: AbilityRuntimeParams.Actor,
                        targetDirection: targetDirection,
                        abilityType: Description.abilityType
                        // targetPosition: targetPosition,
                        // startPosition: startPosition,
                        // effectType: hitEffect.EffectType,
                    );

                TargetEffect.GetEffectByType(hitEffect.EffectType, runtimeParams).Run();
            }
        }

        public static AbilityBase CreateAbility(ref AbilityRuntimeParams runtimeParams)
        {
            if (GameDataManager.TryGetAbilityDescriptionByType(
                runtimeParams.AbilityType, out var abilityDescription))
            {
                return GetAbilityByEffectType(abilityDescription.effect, ref runtimeParams);
            }
            else
            {
                throw new ArgumentException("Unhandled AbilityType");
            }
        }

        private static AbilityBase GetAbilityByEffectType(AbilityEffectType effectType,
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
                AbilityEffectType.ChargeAoeOneShot => new ChargedAoeAbility(ref runtimeParams),
                _ => throw new Exception("Unhandled AbilityEffectType"),
            };
        }
    }
}