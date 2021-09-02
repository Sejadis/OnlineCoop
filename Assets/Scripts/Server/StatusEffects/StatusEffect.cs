using System;
using MLAPI.Spawning;
using Server.Character;
using Shared.Data;
using Shared.StatusEffects;

namespace Server.StatusEffects
{
    public abstract class StatusEffect : Runnable.Runnable
    {
        protected ulong actor => target.NetworkObjectId;
        protected ulong source => runtimeParams.source;

        protected ServerCharacter target =>
            NetworkSpawnManager.SpawnedObjects.TryGetValue(runtimeParams.target, out var netObj)
                ? netObj.GetComponent<ServerCharacter>()
                : null;

        protected StatusEffectType type => runtimeParams.EffectType;
        protected StatusEffectRuntimeParams runtimeParams;


        public StatusEffectDescription Description
        {
            get
            {
                //TODO cache the result
                if (GameDataManager.TryGetStatusEffectDescriptionByType(type,
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

        public override bool Start()
        {
            target.NetworkCharacterState.StatusEffectAddedClientRpc(runtimeParams);
            return true;
        }

        protected StatusEffect(ref StatusEffectRuntimeParams runtimeParams)
        {
            this.runtimeParams = runtimeParams;
        }

        public static StatusEffect CreateStatusEffect(ref StatusEffectRuntimeParams runtimeParams)
        {
            return runtimeParams.EffectType switch
            {
                StatusEffectType.TouchOfLife => new OverTimeStatusEffect(ref runtimeParams),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}