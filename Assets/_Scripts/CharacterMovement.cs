using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] float groundSpeed = 9f;
    [SerializeField] float playerGravity = -9.81f;
    Vector3 yVelocity = Vector3.zero;
    [SerializeField] float downwardPushConstant = -4f;

    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] LayerMask groundMask;

    [SerializeField]
    [Range(0, 0.90f)]
    float _decayFactor = 0.6f;

    float _inputX;
    float _inputY;

    private Vector3 _movement;
    Vector3 _impulse;
    Vector3 _facingDirection;

    CharacterController _characterController;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        HandleInput();
        var horizontal = HandleHorizontalMovement();
        var vertical = HandleVerticalMovement();

        _movement = horizontal + vertical;

        _characterController.Move(_movement);
        _characterController.SimpleMove(_impulse);

        DecayImpulse();
    }

    public void Knockback(Vector3 dir, float force)
    {
        var impulse = dir * force;
        _impulse = impulse;
    }

    private void HandleInput()
    {
        _inputX = Input.GetAxis("Horizontal");
        _inputY = Input.GetAxis("Vertical");
    }

    private void DecayImpulse()
    {
        // decay the impulse force a little every frame
        _impulse *= (1 -_decayFactor);
        if (_impulse.magnitude < 0.01f)
            _impulse = Vector3.zero;
    }

    /// <summary>
    /// Returns the horizontal movement for this frame
    /// </summary>
    /// <returns></returns>
    private Vector3 HandleHorizontalMovement()
    {
        float moveSpeed = groundSpeed;
        float maxDistance = moveSpeed * Time.deltaTime;

        float dX = _inputX * moveSpeed * Time.deltaTime;
        float dZ = _inputY * moveSpeed * Time.deltaTime;

        Vector3 dir = new Vector3(dX, 0, dZ).normalized;

        var movement = Vector3.ClampMagnitude(Vector3.forward * dZ + Vector3.right * dX, maxDistance);

        _characterController.Move(_movement);
        FaceDir(new Vector3(_inputX, 0, _inputY));

        return movement;
    }

    /// <summary>
    /// Returns the vertical movement for this frame
    /// </summary>
    /// <returns></returns>
    private Vector3 HandleVerticalMovement()
    {
        if (isGrounded() && yVelocity.y < 0)
        {
            yVelocity.y = downwardPushConstant;
        }

        Vector3 initYVel = yVelocity;
        yVelocity.y += playerGravity * Time.deltaTime;

        return yVelocity * Time.deltaTime;
    }

    private void FaceDir(Vector3 dir)
    {
        if(dir.magnitude >= 0.1f)
            _facingDirection = dir;

        var angle = Vector3.SignedAngle(transform.forward, _facingDirection, Vector3.up);
        transform.Rotate(0, angle, 0);
    }

    bool isGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }

}
