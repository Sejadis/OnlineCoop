using System;
using Shared.Abilities;
using Shared.Data;

namespace Server.Ability
{
    public abstract class Ability
    {
        public float StartTime { get; set; } = -1f;
        public bool IsStarted => StartTime > 0;
        protected AbilityRuntimeParams abilityRuntimeParams;

        public Ability(ref AbilityRuntimeParams abilityRuntimeParams)
        {
            this.abilityRuntimeParams = abilityRuntimeParams;
        }

        public AbilityDescription Description
        {
            get
            {
                if (GameDataManager.Instance.TryGetAbilityDescriptionByType(abilityRuntimeParams.AbilityType,
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

        public abstract bool Start();

        public abstract bool Update();

        public abstract void End();
        public abstract bool IsBlocking();

        public static Ability CreateAbility(ref AbilityRuntimeParams runtimeParams)
        {
            if (!GameDataManager.Instance.TryGetAbilityDescriptionByType(runtimeParams.AbilityType,
                out var abilityDescription))
            {
                throw new ArgumentException("Unhandled AbilityType.");
            }

            return GetAbilityByEffectType(abilityDescription.effect, ref runtimeParams);
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
                _ => throw new Exception("Unhandled AbilityEffectType"),
            };
        }
    }
}