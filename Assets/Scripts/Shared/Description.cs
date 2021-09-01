using UnityEngine;

namespace Shared
{
    public abstract class Description : ScriptableObject
    {
        public new string name;
        public string description;
        public Sprite icon;
    }
}