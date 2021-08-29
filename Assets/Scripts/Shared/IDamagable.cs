using System;

namespace Shared
{
    public interface IDamagable
    {
        void Damage(ulong actorId, int amount);
        Action<ulong, ulong> OnDeath { get; set; }
    }
}