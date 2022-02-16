using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] float groundSpeed = 9f;
    [SerializeField] float airSpeed = 1f;
    [SerializeField] float playerGravity = -9.81f;
    [SerializeField] float jumpHeight = 3f;
    Vector3 yVelocity = Vector3.zero;
    Vector3 xzVelocity = Vector3.zero;
    [SerializeField] float downwardPushConstant = -4f;

    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] LayerMask groundMask;

    CharacterController _characterController;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrounded())
            HorizontalMovement();

        VerticalMovement();
    }

    //  Handles all horizontal movement
    private Vector3 movement;
    private void HorizontalMovement()
    {
        float moveSpeed = groundSpeed;
        float maxDistance = moveSpeed * Time.deltaTime;

        float dX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float dZ = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        movement = Vector3.ClampMagnitude(transform.forward * dZ + transform.right * dX, maxDistance);

        _characterController.Move(movement);
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
