using System;
using MLAPI;
using Shared.Settings;
using UnityEngine;

namespace Server.Character
{
    [RequireComponent(typeof(CharacterController))]
    public class ServerCharacterMovement : NetworkBehaviour
    {
        [SerializeField] private CharacterController characterController;
        [SerializeField] private Transform followTarget;

        public float fixedGroundedGravity = -5f;

        public event Action OnMovementStarted;
        private Vector2 moveInput;

        public Vector2 MoveInput
        {
            get => moveInput;
            set
            {
                if (moveInput == Vector2.zero && value != Vector2.zero)
                {
                    //we had no input and got input
                    //notify about starting to move
                    //TODO move away from input, as there are other reasons why we may be standing still despite move input
                    OnMovementStarted?.Invoke();
                }
                //do we need a notification when we did move and stopped?

                moveInput = value;
            }
        }

        public bool IsSprinting { get; set; }
        public Vector2 lookInput { get; set; }
        public bool IsNPC { get; set; } = true;
        public bool JumpRequested { get; set; }

        //TODO refactor into net state
        [SerializeField] private float jumpHeight = 3;
        [SerializeField] private float groundCheckRadius = 0.05f;
        [SerializeField] private SettingFloat sensitivity;
        protected float moveSpeed = 5;
        protected float sprintSpeedMultiplier = 2;
        private float gravity = Physics.gravity.y * 2f;

        private float yRotation;
        private float xRotation;
        private bool isGrounded;
        private Vector3 yVelocity;

        private bool isForceMoving;
        private Vector3 forceMoveTargetPosition;
        private float forceMoveSpeed;

        public void Teleport(Vector3 targetPosition)
        {
            throw new NotImplementedException();
        }

        public void ForceMovement(Vector3 targetDirection, float speed)
        {
            isForceMoving = true;
            forceMoveSpeed = speed;
            forceMoveTargetPosition = transform.position + targetDirection;
            forceMoveTargetPosition.y =
                transform.position.y; //TODO change when force movement allows for vertical pushes
        }

        public void ForceMovement(Vector3 direction, float speed, float time)
        {
            throw new NotImplementedException();
        }

        public override void NetworkStart()
        {
            base.NetworkStart();
            if (!IsServer)
            {
                enabled = false;
                return;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (isForceMoving && Vector3.Distance(transform.position, forceMoveTargetPosition) < 0.5f)
            {
                isForceMoving = false;
            }

            GroundCheck();
            characterController.stepOffset = isGrounded ? 0.3f : 0f;
            ApplyMovement();
            if (!IsNPC)
            {
                yRotation += lookInput.x * sensitivity.Value;
                yRotation %= 360; //keep the number small

                transform.rotation =
                    Quaternion.Euler(transform.rotation.eulerAngles.x, yRotation, transform.rotation.eulerAngles.z); 

                xRotation += lookInput.y * sensitivity.Value;
                xRotation = Mathf.Clamp(xRotation, -90f, 70f);
                followTarget.rotation = Quaternion.Euler(xRotation, followTarget.rotation.eulerAngles.y,
                    followTarget.rotation.eulerAngles.z);
            }

            if (JumpRequested)
            {
                if (isGrounded)
                {
                    Jump();
                }

                JumpRequested = false;
            }

            ApplyGravity();
        }

        private void GroundCheck()
        {
            var pos = transform.position;
            pos.y -= characterController.skinWidth;
            isGrounded = Physics.CheckSphere(pos, groundCheckRadius,
                1 << LayerMask.NameToLayer("Ground"));
        }

        private void OnDrawGizmos()
        {
            var pos = transform.position;
            pos.y -= characterController.skinWidth;
            Gizmos.DrawSphere(pos, groundCheckRadius);
        }

        protected virtual void ApplyMovement()
        {
            Vector3 finalMove;
            if (isForceMoving)
            {
                var targetDirection = (forceMoveTargetPosition - transform.position).normalized;
                finalMove = targetDirection * (Time.deltaTime * forceMoveSpeed);
            }
            else
            {
                finalMove = MoveInput.x * transform.right;
                finalMove += MoveInput.y * transform.forward;
                finalMove.Normalize();
                finalMove *= IsSprinting ? moveSpeed * sprintSpeedMultiplier : moveSpeed;
                finalMove *= Time.deltaTime;
            }

            characterController.Move(finalMove);
        }

        private void Jump()
        {
            // yVelocity.y = jumpHeight;
            yVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            // JumpRequested = false;
        }

        private void ApplyGravity()
        {
            if (isGrounded && yVelocity.y <= 0)
            {
                yVelocity.y = fixedGroundedGravity;
            }
            else
            {
                yVelocity.y += gravity * Time.deltaTime;
            }

            characterController.Move(yVelocity * Time.deltaTime);
        }
    }
}