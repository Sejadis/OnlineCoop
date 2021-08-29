using MLAPI.Serialization;
using UnityEngine;

namespace Shared.Abilities
{
    public struct AbilityRuntimeParams : INetworkSerializable
    {
        public AbilityType AbilityType;
        public ulong Actor;
        public ulong TargetEntity;
        public Vector3 TargetPosition;
        public Vector3 TargetDirection;
        public Vector3 StartPosition;


        public AbilityRuntimeParams(AbilityType abilityType, ulong actor, ulong targetEntity, Vector3 targetPosition, Vector3 targetDirection, Vector3 startPosition)
        {
            AbilityType = abilityType;
            Actor = actor;
            TargetEntity = targetEntity;
            TargetPosition = targetPosition;
            TargetDirection = targetDirection;
            StartPosition = startPosition;
        }

        public AbilityRuntimeParams(AbilityRuntimeParams abilityRuntimeParams)
        {
            AbilityType = abilityRuntimeParams.AbilityType;
            Actor = abilityRuntimeParams.Actor;
            TargetEntity = abilityRuntimeParams.TargetEntity;
            TargetPosition = abilityRuntimeParams.TargetPosition;
            TargetDirection = abilityRuntimeParams.TargetDirection;
            StartPosition = abilityRuntimeParams.StartPosition;
        }

        public void NetworkSerialize(NetworkSerializer serializer)
        {
            serializer.Serialize(ref AbilityType);
            serializer.Serialize(ref Actor);
            serializer.Serialize(ref TargetEntity);
            serializer.Serialize(ref TargetPosition);
            serializer.Serialize(ref TargetDirection);
            serializer.Serialize(ref StartPosition);
        }
    }

}
