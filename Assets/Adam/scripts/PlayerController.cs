using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float jumpForce = 10f;

    private bool isRunning = false;
    private bool isGrounded;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Check if the character is on the ground
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.1f);

        // Get input from the arrow keys
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Get whether the left shift key is pressed
        isRunning = Input.GetKey(KeyCode.LeftShift);

        // Calculate movement direction
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);
        movement.Normalize(); // Normalize to prevent faster diagonal movement

        // Apply movement to the object
        Move(movement);

        // Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
    }

    void Move(Vector3 direction)
    {
        // Calculate movement speed
        float speed = isRunning ? runSpeed : walkSpeed;

        // Calculate movement vector
        Vector3 movement = direction * speed * Time.deltaTime;

        // Apply movement to the object
        transform.Translate(movement);
    }

    void Jump()
    {
        // Apply upward force for jumping
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
