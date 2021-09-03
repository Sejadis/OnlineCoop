using MLAPI.Serialization;
using Server.TargetEffects;
using UnityEngine;

namespace Shared.Abilities
{
    public struct AbilityRuntimeParams : INetworkSerializable
    {
        public AbilityType AbilityType;
        // public TargetEffectType EffectType;
        public ulong Actor;
        public ulong[] Targets;
        public Vector3 TargetPosition;
        public Vector3 TargetDirection;
        public Vector3 StartPosition;


        public AbilityRuntimeParams(AbilityType abilityType, ulong actor,
            ulong[] targets, Vector3 targetPosition, Vector3 targetDirection, Vector3 startPosition, TargetEffectType effectType = TargetEffectType.None)
        {
            AbilityType = abilityType;
            // EffectType = effectType;
            Actor = actor;
            Targets = targets;
            TargetPosition = targetPosition;
            TargetDirection = targetDirection;
            StartPosition = startPosition;
        }

        public AbilityRuntimeParams(AbilityRuntimeParams abilityRuntimeParams)
        {
            AbilityType = abilityRuntimeParams.AbilityType;
            // EffectType = abilityRuntimeParams.EffectType;
            Actor = abilityRuntimeParams.Actor;
            Targets = abilityRuntimeParams.Targets;
            TargetPosition = abilityRuntimeParams.TargetPosition;
            TargetDirection = abilityRuntimeParams.TargetDirection;
            StartPosition = abilityRuntimeParams.StartPosition;
        }

        public void NetworkSerialize(NetworkSerializer serializer)
        {
            serializer.Serialize(ref AbilityType);
            // serializer.Serialize(ref EffectType);
            serializer.Serialize(ref Actor);
            serializer.Serialize(ref Targets);
            serializer.Serialize(ref TargetPosition);
            serializer.Serialize(ref TargetDirection);
            serializer.Serialize(ref StartPosition);
        }
    }
}