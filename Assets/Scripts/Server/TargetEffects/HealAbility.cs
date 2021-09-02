﻿using MLAPI.Spawning;
using Shared;
using Shared.Abilities;

namespace Server.Ability.TargetEffects
{
    public class HealAbility : TargetEffect
    {
        public HealAbility(TargetEffectParameter targetEffectParameter) : base(targetEffectParameter)
        {
        }

        public override void Run()
        {
            if (NetworkSpawnManager.SpawnedObjects.TryGetValue(EffectParameter.Target, out var netObj))
            {
                netObj.GetComponent<IHealable>()?.Heal(EffectParameter.Actor, (int) SourceDescription.mainValue);
            }
        }
    }
}