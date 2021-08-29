using UnityEngine;

namespace Shared
{
    public static class Utility
    {
        public static Vector2 GetRelativeMouseDelta(Vector2 mouseDelta)
        {
            Vector2 result = new Vector2();
            result.x = mouseDelta.x / Screen.width;
            result.y = mouseDelta.y / Screen.height;
            return result;
        }
    }
}