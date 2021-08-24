using System.Collections;
using System.Collections.Generic;
using MLAPI.Serialization;
using UnityEngine;

namespace SejDev.Systems.Ability
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
