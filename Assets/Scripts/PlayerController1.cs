using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController1 : MonoBehaviour
{
    public float speed = 6.0f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.0f;
    public GameObject staticAnimator; // Reference to the StaticAnimator GameObject

    private Vector3 playerVelocity;
    private bool isGrounded;
    private CharacterController controller;
    private Animator animator; // Reference to the Animator component

    void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();

        controller.height = 0;
        controller.center = new Vector3(0, 0, 0);
        controller.radius = 0;

        if (staticAnimator != null)
        {
            animator = staticAnimator.GetComponent<Animator>();
        }
    }

    void Update()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Calculate movement vector
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // Set isRunning parameter in Animator before applying movement
        if (animator != null)
        {
            bool isRunning = move.magnitude > 0;
            animator.SetBool("isRunning", isRunning);
        }

        // Apply movement
        controller.Move(move * speed * Time.deltaTime);

        // Handle jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Apply gravity
        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
