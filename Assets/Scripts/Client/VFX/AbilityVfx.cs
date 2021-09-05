using System;
using Shared.Abilities;
using Shared.Data;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Client.VFX
{
    public abstract class AbilityVfx : AbilityBase
    {
        private Transform effectTransform;

        protected Transform EffectTransform => effectTransform;

        public AbilityVfx(ref AbilityRuntimeParams abilityRuntimeParams) : base(
            ref abilityRuntimeParams)
        {
        }

        public static AbilityVfx CreateVfx(ref AbilityRuntimeParams runtimeParams)
        {
            if (GameDataManager.TryGetAbilityDescriptionByType(
                runtimeParams.AbilityType, out var abilityDescription))
            {
                return GetAbilityByEffectType(abilityDescription, ref runtimeParams);
            }
            else
            {
                throw new ArgumentException("Unhandled AbilityType when creating VFX");
            }
        }

        protected void SpawnPrefab(int prefabIndex = 0)
        {
            effectTransform = Object.Instantiate(Description.Prefabs[prefabIndex], AbilityRuntimeParams.TargetPosition,
                Quaternion.identity).transform;
        }

        protected virtual bool TrySpawnPrefab(int prefabIndex = 0)
        {
            if (effectTransform == null && StartTime + Description.castTime <= Time.time)
            {
                SpawnPrefab(prefabIndex);
                return true;
            }

            return false;
        }

        public override bool Update()
        {
            TrySpawnPrefab();
            return true;
        }

        public override void End()
        {
            //by default just destroy the spawned object
            Object.Destroy(EffectTransform.gameObject);
        }

        private static AbilityVfx GetAbilityByEffectType(AbilityDescription description,
            ref AbilityRuntimeParams runtimeParams)
        {
            return description.effect switch
            {
                // AbilityEffectType.Projectile => new ProjectileAbility(ref runtimeParams),
                // AbilityEffectType.Craft => new CraftItemAbility(ref runtimeParams),
                AbilityEffectType.AoeZone => new AoeZoneVfx(ref runtimeParams),
                // AbilityEffectType.AoeOneShot => new AoeAbility(ref runtimeParams),
                // AbilityEffectType.Log => new LogAbility(ref runtimeParams),
                // AbilityEffectType.SpawnObject => new SpawnObjectAbility(ref runtimeParams),
                AbilityEffectType.ChargeAoeOneShot => new ChargeVfx(ref runtimeParams),
                _ => throw new Exception("Unhandled AbilityEffectType when creating VFX"),
            };
        }
    }
}