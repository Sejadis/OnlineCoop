using System;
using MLAPI;
using MLAPI.Spawning;
using TreeEditor;
using UnityEngine;

namespace SejDev.Systems.Ability
{
    public class AoeAbilityInput : AbilityInput
    {
        [SerializeField] private GameObject targetVisualParent;
        [SerializeField] private GameObject targetVisualInRange;
        [SerializeField] private GameObject targetVisualOutOfRange;

        private Camera camera;
        private Vector3 screenCenter;
        private LayerMask groundLayerMask;
        private Vector3 newPos;

        private void Start()
        {
            camera = Camera.main;
            screenCenter = new Vector3(camera.pixelWidth / 2, camera.pixelHeight / 2, 0);
            groundLayerMask = 1 << 8;
            transform.parent = baseTransform.parent;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }

        private void GetTargetPosition()
        {
            var ray = new Ray(baseTransform.position, baseTransform.forward);
            if (Physics.Raycast(ray, out var hit, abilityDescription.range, groundLayerMask))
            {
                // Vector2 hitInXZPlane = new Vector2(hit.point.x, hit.point.z);
                // var position = camera.transform.position;
                // Vector2 cameraInXZPlane = new Vector2(position.x, position.z);
                // float distance = Vector2.Distance(hitInXZPlane, cameraInXZPlane);
                // newPos.z = distance;
            }
        }
        private void Update()
        {
            // Ray ray = camera.ScreenPointToRay(screenCenter);

            var ray = new Ray(baseTransform.position, baseTransform.forward);
            if (Physics.Raycast(ray, out var hit, abilityDescription.range, groundLayerMask))
            {
                Vector2 hitInXZPlane = new Vector2(hit.point.x, hit.point.z);
                var position = camera.transform.position;
                Vector2 cameraInXZPlane = new Vector2(position.x, position.z);
                float distance = Vector2.Distance(hitInXZPlane, cameraInXZPlane);
                newPos.z = distance;
            }
            else
            {
                newPos.z = abilityDescription.range;
            }

            targetVisualParent.transform.localPosition = newPos;
        }
    }
}