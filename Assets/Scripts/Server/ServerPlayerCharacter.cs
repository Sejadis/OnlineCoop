using MLAPI;
using UnityEngine;

namespace Server
{
    public class ServerPlayerCharacter : ServerCharacter
    {
        [SerializeField] private Transform followTarget;

        public Vector2 lookInput;
        public float sensitivity = 1f;
        public float yRotation;
        public float xRotation;

        // Start is called before the first frame update
        public override void NetworkStart()
        {
            base.NetworkStart();
            networkCharacterState.OnMoveInputReceived += OnMoveInputReceived;
            networkCharacterState.OnLookInputReceived += OnLookInputReceived;
            networkCharacterState.OnSprintReceived += OnSprintReceived;

            yRotation = transform.rotation.eulerAngles.y;
            xRotation = followTarget.rotation.eulerAngles.x;
        }
        private void OnSprintReceived(bool sprint)
        {
            networkCharacterState.IsSprinting.Value = sprint;
        }

        private void OnMoveInputReceived(Vector2 input)
        {
            moveInput = input;
        }

        private void OnLookInputReceived(Vector2 input)
        {
            lookInput = input;
        }

        // Update is called once per frame
        protected override void Update()
        {
            if (!IsOwner)
            {
                //give ownership to server (which i think is always 0?)
                GetComponent<NetworkObject>().ChangeOwnership(0);
            }
            base.Update();

            yRotation += lookInput.x * sensitivity;
            yRotation %= 360; //keep the number small

            transform.rotation =
                Quaternion.Euler(transform.rotation.eulerAngles.x, yRotation, transform.rotation.eulerAngles.z);
        
            xRotation += lookInput.y * sensitivity;
            xRotation = Mathf.Clamp(xRotation, -90f, 70f);
            followTarget.rotation = Quaternion.Euler(xRotation, followTarget.rotation.eulerAngles.y,
                followTarget.rotation.eulerAngles.z);
        }
    }
}