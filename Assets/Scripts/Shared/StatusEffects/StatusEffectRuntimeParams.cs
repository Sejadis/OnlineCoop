using MLAPI.Serialization;

namespace Shared.StatusEffects
{
    public struct StatusEffectRuntimeParams : INetworkSerializable
    {
        public StatusEffectType EffectType;
        public ulong source;
        public ulong[] targets;
        public void NetworkSerialize(NetworkSerializer serializer)
        {
            serializer.Serialize(ref EffectType);
            serializer.Serialize(ref source);
            serializer.Serialize(ref targets);
        }
    }
}