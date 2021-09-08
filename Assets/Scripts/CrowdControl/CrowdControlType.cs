using System;

namespace CrowdControl
{
    [Flags]
    public enum CrowdControlType
    {
        None = 0,
        Stun = 1 << 0,
        Root = 1 << 1,
        Silence = 1 << 2,
    }
}