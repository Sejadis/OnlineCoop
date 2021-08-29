using System;

namespace Shared
{
    [Flags]
    public enum CrowdControl
    {
        None = 0,
        Root = 1 << 0,
        Stun = 1 << 1,
        Silence = 1 << 2,
    }
}