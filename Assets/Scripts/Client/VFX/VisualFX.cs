using Shared.Abilities;
using UnityEngine;

namespace Client.VFX
{
    public abstract class VisualFX : MonoBehaviour
    {
        public abstract void Init(ref AbilityRuntimeParams runtimeParams);
    }
}