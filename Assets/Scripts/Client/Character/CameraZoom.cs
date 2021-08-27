using Cinemachine;
using Client.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Client.Character
{
    public class CameraZoom : MonoBehaviour
    {
        [SerializeField] private float zoomStep;
        [SerializeField] private float minZoom;
        [SerializeField] private float maxZoom;

        private Cinemachine3rdPersonFollow virtualCamera;

        private float cameraDistance;
        // Start is called before the first frame update
        void Start()
        {
            virtualCamera = GetComponent<CinemachineVirtualCamera>()?.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
        
            if (virtualCamera == null)
            {
                enabled = false;
                return;
            }

            cameraDistance = virtualCamera.CameraDistance;
            InputManager.Instance.OnZoom += OnZoom;
        }

        private void OnZoom(InputAction.CallbackContext obj)
        {
            var val = obj.ReadValue<float>();
            cameraDistance += val * zoomStep;
            cameraDistance = Mathf.Clamp(cameraDistance, minZoom, maxZoom);
            virtualCamera.CameraDistance = cameraDistance;
        }
    }
}
