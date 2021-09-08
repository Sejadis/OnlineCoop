using Shared.Abilities;
using UnityEngine;

namespace Client.Ability
{
    public class AoeAbilityInput : AbilityInput
    {
        [SerializeField] private GameObject targetVisualParent;
        [SerializeField] private GameObject targetVisualInRange;
        [SerializeField] private GameObject targetVisualOutOfRange;

        private Camera targetingCamera;
        private Vector3 screenCenter;
        private static LayerMask groundLayerMask = 1 << 8;
        private Vector3 newPos;

        private void Awake()
        {
            targetingCamera = Camera.main;
        }

        public override void GetRuntimeParams(ref AbilityRuntimeParams runtimeParams)
        {
            var position = GetValidPosition();
            runtimeParams.TargetPosition = position;
            runtimeParams.TargetDirection = runtimeParams.TargetPosition - runtimeParams.StartPosition;
        }

        public override void GetRuntimeParams(ref AbilityRuntimeParams runtimeParams, Camera camera,
            Transform baseTransform, AbilityDescription abilityDescription)
        {
            var position = GetValidPosition(camera, baseTransform, abilityDescription.range);
            runtimeParams.TargetPosition = position;
            runtimeParams.TargetDirection = runtimeParams.TargetPosition - runtimeParams.StartPosition;
        }

        private void Update()
        {
            targetVisualParent.transform.position = GetValidPosition();
        }

        private Vector3 GetValidPosition()
        {
            return GetValidPosition(targetingCamera, baseTransform, abilityDescription.range);
        }

        private Vector3 GetValidPosition(Camera camera, Transform baseTransform, float abilityRange)
        {
            screenCenter = new Vector3(camera.pixelWidth / 2, camera.pixelHeight / 2, 0);
            var ray = camera.ScreenPointToRay(screenCenter);
            var camToPlayerDistance = Vector3.Distance(camera.transform.position, baseTransform.position);
            var range = abilityRange + camToPlayerDistance;
            Debug.DrawRay(ray.origin, ray.direction * range, Color.green, 2f);
            if (Physics.Raycast(ray, out var hitInfo, range, groundLayerMask))
            {
                return hitInfo.point;
            }
            else
            {
                var rayStart = camera.transform.position + ray.direction * range;
                Debug.DrawRay(rayStart, Vector3.down * 100f, Color.red, 2f);
                if (Physics.Raycast(rayStart, Vector3.down, out hitInfo, float.PositiveInfinity, groundLayerMask))
                {
                    return hitInfo.point;
                }
            }

            return Vector3.zero;
        }
    }
}