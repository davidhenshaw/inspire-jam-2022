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

    float _inputX;
    float _inputY;

    Vector3 _facingDirection;

    CharacterController _characterController;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        HorizontalMovement();
        VerticalMovement();
    }

    private void HandleInput()
    {
        _inputX = Input.GetAxis("Horizontal");
        _inputY = Input.GetAxis("Vertical");
    }

    //  Handles all horizontal movement
    private Vector3 _movement;
    private void HorizontalMovement()
    {
        float moveSpeed = groundSpeed;
        float maxDistance = moveSpeed * Time.deltaTime;

        float dX = _inputX * moveSpeed * Time.deltaTime;
        float dZ = _inputY * moveSpeed * Time.deltaTime;

        _movement = Vector3.ClampMagnitude(Vector3.forward * dZ + Vector3.right * dX, maxDistance);

        _characterController.Move(_movement);
        FaceDir(new Vector3(_inputX, 0, _inputY));
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

    // Handles all vertical movement
    void VerticalMovement()
    {
        if (isGrounded() && yVelocity.y < 0)
        {
            yVelocity.y = downwardPushConstant;
        }

        Vector3 initYVel = yVelocity;
        yVelocity.y += playerGravity * Time.deltaTime;

        _characterController.Move(yVelocity * Time.deltaTime);
    }

}