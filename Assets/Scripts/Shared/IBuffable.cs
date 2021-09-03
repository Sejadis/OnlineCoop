using Shared.StatusEffects;

namespace Shared
{
    public interface IBuffable
    {
        void AddStatusEffect(ref StatusEffectRuntimeParams runtimeParams);
    }
}