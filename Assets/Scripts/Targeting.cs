using Shared.Abilities;
using UnityEngine;

namespace DefaultNamespace
{
    public static class Targeting
    {
        public static bool GetGroundTarget(ref AbilityDescription abilityDescription,
            ref AbilityRuntimeParams runtimeParams)
        {
            var cam = Camera.main;
            var screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);
            var ray = cam.ScreenPointToRay(screenCenter);
            var groundLayerMask = 1 << LayerMask.NameToLayer("Ground");
            var camToPlayerDistance = Vector3.Distance(cam.transform.position, runtimeParams.StartPosition);
            var range = abilityDescription.range + camToPlayerDistance;
            Debug.DrawRay(ray.origin, ray.direction * range, Color.green, 5f);
            if (Physics.Raycast(ray, out var hitInfo, range, groundLayerMask))
            {
                runtimeParams.TargetDirection = hitInfo.point - runtimeParams.StartPosition;
                runtimeParams.TargetPosition = hitInfo.point;
                return true;
            }
            else
            {
                var rayStart = cam.transform.position + ray.direction * range;
                Debug.DrawRay(rayStart, Vector3.down * 100f, Color.red, 5f);
                if (Physics.Raycast(rayStart, Vector3.down, out hitInfo, float.PositiveInfinity, groundLayerMask))
                {
                    runtimeParams.TargetDirection = hitInfo.point - runtimeParams.StartPosition;
                    runtimeParams.TargetPosition = hitInfo.point;
                    return true;
                }
            }

            return false;
        }
    }
}