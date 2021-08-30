using System;

namespace Shared
{
    public interface IDamagable
    {
        void Damage(ulong actorId, int amount);
        event Action<ulong, ulong> OnDeath;
    }
}