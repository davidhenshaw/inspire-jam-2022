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

    [SerializeField]
    float _dashSpeed = 15f;
    [SerializeField]
    [Range(0, 1)]
    float _dashDuration = 0.33f;
    float _cooldownDuration = 0.5f;
    bool _isDashing = false;
    bool _cooldown = false;

    float _inputX;
    float _inputY;

    /// <summary>
    /// This transform is used to ensure the player moves in a direction relative to how the camera views them
    /// </summary>
    public Transform RelativeTransform;
    private Vector3 _movement;
    Vector3 _impulse;
    Vector3 _facingDirection;

    CharacterController _characterController;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();

        if (!RelativeTransform)
            RelativeTransform = Camera.main.transform;
    }

    void Update()
    {
        HandleInput();
        var horizontal = HandleHorizontalMovement();
        var vertical = HandleVerticalMovement();

        _movement = horizontal + vertical;

        if (_isDashing)
            _movement = HandleDash();

        _characterController.Move(_movement);
        _characterController.SimpleMove(_impulse);

        DecayImpulse();
    }

    public void Knockback(Vector3 dir, float force)
    {
        var impulse = dir * force;
        _impulse = impulse;
    }

    public IEnumerator Dash_co()
    {
        _isDashing = true;
        yield return new WaitForSeconds(_dashDuration);
        _isDashing = false;
        StartCoroutine(Cooldown_co());
    }

    IEnumerator Cooldown_co()
    {
        _cooldown = true;
        yield return new WaitForSeconds(_cooldownDuration);
        _cooldown = false;
    }

    private void HandleInput()
    {
        _inputX = Input.GetAxis("Horizontal");
        _inputY = Input.GetAxis("Vertical");

        if(Input.GetKeyDown(KeyCode.LeftShift) && !_isDashing && !_cooldown)
        {
            StartCoroutine(Dash_co());
        }
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

        var movement = Vector3.ClampMagnitude(RelativeTransform.forward * dZ + RelativeTransform.right * dX, maxDistance);

        _characterController.Move(_movement);

        var inputVec = new Vector3(_inputX, 0, _inputY);
        var relativeInputX = Vector3.Dot(inputVec, RelativeTransform.right);
        var relativeInputZ = Vector3.Dot(inputVec, RelativeTransform.forward);
        
        FaceDir(new Vector3(relativeInputX, 0, relativeInputZ) * -1);

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

    /// <summary>
    /// Returns the movement this frame for a dash
    /// </summary>
    /// <returns></returns>
    private Vector3 HandleDash()
    {
        var result = _facingDirection * _dashSpeed * Time.deltaTime;
        return result;
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
