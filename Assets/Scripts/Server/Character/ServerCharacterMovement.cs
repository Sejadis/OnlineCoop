using System;
using MLAPI;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class ServerCharacterMovement : NetworkBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Transform followTarget;

    public float fixedGroundedGravity = -5f;
    public Vector2 moveInput { get; set; }
    public bool IsSprinting { get; set; }
    public Vector2 lookInput { get; set; }
    public bool IsNPC { get; set; } = true;
    public bool JumpRequested { get; set; }

    //TODO refactor into net state
    [SerializeField] private float jumpHeight = 3;
    [SerializeField] private float groundCheckRadius = 0.05f;
    protected float moveSpeed = 5;
    protected float sprintSpeedMultiplier = 2;
    private float sensitivity = 1f;
    private float gravity = Physics.gravity.y * 2f;
    private bool gorund;


    private float yRotation;
    private float xRotation;
    private bool isGrounded;
    private Vector3 yVelocity;

    public void Teleport(Vector3 targetPosition)
    {
        throw new NotImplementedException();
    }

    public void ForceMovement(Vector3 targetPosition)
    {
        throw new NotImplementedException();
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
        gorund = characterController.isGrounded;
        GroundCheck();
        characterController.stepOffset = isGrounded ? 0.3f : 0f;
        ApplyMovement();
        if (!IsNPC)
        {
            yRotation += lookInput.x * sensitivity;
            yRotation %= 360; //keep the number small

            transform.rotation =
                Quaternion.Euler(transform.rotation.eulerAngles.x, yRotation, transform.rotation.eulerAngles.z);

            xRotation += lookInput.y * sensitivity;
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
        var finalMove = moveInput.x * transform.right;
        finalMove += moveInput.y * transform.forward;
        finalMove.Normalize();
        finalMove *= IsSprinting ? moveSpeed * sprintSpeedMultiplier : moveSpeed;
        finalMove *= Time.deltaTime;
        // transform.position += finalMove;

        characterController.Move(finalMove);
    }

    private void Jump()
    {
        // yVelocity.y = jumpHeight;
        yVelocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity);
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