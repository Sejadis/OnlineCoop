using System;
using DefaultNamespace;
using Shared.Abilities;
using Shared.Data;
using UnityEngine;

public abstract class AbilityBase : Runnable
{
    private readonly AbilityRuntimeParams abilityRuntimeParams;
    public AbilityRuntimeParams AbilityRuntimeParams => abilityRuntimeParams;
    private bool didCooldownStart = false;
    protected virtual bool CanStartCooldown { get; set; } = true;

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

    public bool IsBlocking()
    {
        return Description.castTime > 0 && Time.time - StartTime < Description.castTime;
    }

    public bool ShouldStartCooldown()
    {
        var currentValue = didCooldownStart;
        didCooldownStart = didCooldownStart || CanStartCooldown;
        return AbilityRuntimeParams.EffectType == TargetEffectType.None && !currentValue && CanStartCooldown;
    }

    public AbilityBase(ref AbilityRuntimeParams abilityRuntimeParams)
    {
        this.abilityRuntimeParams = abilityRuntimeParams;
    }
}