using MLAPI;
using UnityEngine;

namespace Server
{
    public class ServerPlayerCharacter : ServerCharacter
    {
        [SerializeField] private Transform followTarget;
        private bool waitsForMovementReset;

        // Start is called before the first frame update
        public override void NetworkStart()
        {
            base.NetworkStart();
            networkCharacterState.OnMoveInputReceived += OnMoveInputReceived;
            networkCharacterState.OnLookInputReceived += OnLookInputReceived;
            networkCharacterState.OnSprintReceived += OnSprintReceived;
            networkCharacterState.OnJumpReceived += OnJumpReceived;

            //should default to 0 anyway, might need to change later
            // yRotation = transform.rotation.eulerAngles.y;
            // xRotation = followTarget.rotation.eulerAngles.x;
            serverCharacterMovement.IsNPC = false;
        }

        private void OnJumpReceived()
        {
            serverCharacterMovement.JumpRequested = true;
        }

        private void OnSprintReceived(bool sprint)
        {
            networkCharacterState.IsSprinting.Value = sprint;
            serverCharacterMovement.IsSprinting = sprint;
        }

        private void OnMoveInputReceived(Vector2 input)
        {
            if (!waitsForMovementReset || waitsForMovementReset && input == Vector2.zero)
            {
                waitsForMovementReset = false;
                serverCharacterMovement.moveInput = input;
            }
        }

        private void OnLookInputReceived(Vector2 input)
        {
            serverCharacterMovement.lookInput = input;
        }

        public void CancelMovement()
        {
            //we will skip all inputs till we got a zero input
            waitsForMovementReset = serverCharacterMovement.moveInput != Vector2.zero;
            serverCharacterMovement.moveInput = Vector2.zero;
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
        }
    }
}