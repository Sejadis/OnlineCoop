using System;
using Server.Ability;
using Shared.Abilities;
using Shared.Data;

public abstract class AbilityTargetEffect : AbilityBase
{
    public AbilityTargetEffect(ref AbilityRuntimeParams runtimeParams) : base(ref runtimeParams)
    {
    }

    private bool canStartCooldown = false;

    protected sealed override bool CanStartCooldown
    {
        get => canStartCooldown;
        set { } //TODO this is bad, it suggests its allowed to set the value when it isnt
    }

    public sealed override bool Update()
    {
        throw new InvalidOperationException("Update should not be called on AbilityTargetEffects");
    }

    public sealed override void End()
    {
    }

    public sealed override void Cancel()
    {
    }
}