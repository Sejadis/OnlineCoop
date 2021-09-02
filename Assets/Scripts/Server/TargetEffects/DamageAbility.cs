﻿using MLAPI.Spawning;
using Shared;
using Shared.Abilities;
using StatusEffects;

namespace Server.Ability.TargetEffects
{
    public class DamageAbility : TargetEffect
    {
        public DamageAbility(TargetEffectParameter targetEffectParameter) : base(targetEffectParameter)
        {
        }

        public override void Run()
        {
            if (NetworkSpawnManager.SpawnedObjects.TryGetValue(EffectParameter.Target, out var netObj))
            {
                netObj.GetComponent<IDamagable>()?.Damage(EffectParameter.Actor, (int) SourceDescription.mainValue);
            }
        }
    }
}