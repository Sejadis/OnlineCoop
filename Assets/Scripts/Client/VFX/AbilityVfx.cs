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

        public AbilityVfx(ref AbilityRuntimeParams abilityRuntimeParams, Transform effectTransform) : base(
            ref abilityRuntimeParams)
        {
            this.effectTransform = effectTransform;
        }

        public static AbilityVfx CreateVfx(ref AbilityRuntimeParams runtimeParams)
        {
            if (GameDataManager.TryGetAbilityDescriptionByType(
                runtimeParams.AbilityType, out var abilityDescription))
            {
                var obj = Object.Instantiate(abilityDescription.Prefabs[0], runtimeParams.TargetPosition,
                    Quaternion.identity);

                return GetAbilityByEffectType(abilityDescription, ref runtimeParams, obj.transform);
            }
            else
            {
                throw new ArgumentException("Unhandled AbilityType when creating VFX");
            }
        }

        public override void End()
        {
            //by default just destroy the spawned object
            Object.Destroy(EffectTransform.gameObject);
        }

        private static AbilityVfx GetAbilityByEffectType(AbilityDescription description,
            ref AbilityRuntimeParams runtimeParams, Transform effectTransform)
        {
            return description.effect switch
            {
                // AbilityEffectType.Projectile => new ProjectileAbility(ref runtimeParams),
                // AbilityEffectType.Craft => new CraftItemAbility(ref runtimeParams),
                // AbilityEffectType.AoeZone => new AoeZoneAbility(ref runtimeParams),
                // AbilityEffectType.AoeOneShot => new AoeAbility(ref runtimeParams),
                // AbilityEffectType.Log => new LogAbility(ref runtimeParams),
                // AbilityEffectType.SpawnObject => new SpawnObjectAbility(ref runtimeParams),
                AbilityEffectType.ChargeAoeOneShot => new ChargeVFX(ref runtimeParams, effectTransform),
                _ => throw new Exception("Unhandled AbilityEffectType when creating VFX"),
            };
        }
    }
}