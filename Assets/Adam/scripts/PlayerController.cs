using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float fastSpeedMultiplier = 2f;
    public float normalFOV = 60f;
    public float fastFOV = 80f;

    private CharacterController characterController;
    private Camera playerCamera;
    private float originalFOV;
    private bool isGrounded;
    private float turnSmoothVelocity;
    private float turnSmoothTime;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerCamera = Camera.main;
        originalFOV = playerCamera.fieldOfView;
    }

    void Update()
    {
        // Check if the player is grounded
        isGrounded = characterController.isGrounded;

        // Move the player
        MovePlayer();

        // Jump
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        // Sprint (increase speed and FOV)
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed *= fastSpeedMultiplier;
            playerCamera.fieldOfView = fastFOV;
        }
        else
        {
            moveSpeed /= fastSpeedMultiplier;
            playerCamera.fieldOfView = normalFOV;
        }
    }

    private void MovePlayer()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            characterController.Move(moveDirection.normalized * moveSpeed * Time.deltaTime);

            // Rotate the player to face the moving direction
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
    }

    private void Jump()
    {
        characterController.Move(Vector3.up * jumpForce * Time.deltaTime);
    }
}
