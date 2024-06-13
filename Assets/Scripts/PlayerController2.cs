using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    public float speed;
    public float sprintSpeed;
    public float jumpForce;
    public bool isGrounded;
    public GameObject spine;
    public float groundRaycastDistance;

    private Rigidbody spineRigidbody;
    private bool canJump = true;

    void Start()
    {
        if (spine != null)
        {
            spineRigidbody = spine.GetComponent<Rigidbody>();
            spineRigidbody.mass = 1f;
            spineRigidbody.drag = 0f;
            spineRigidbody.angularDrag = 0f;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && canJump)
        {
            StartCoroutine(Jump());
        }
    }

    void FixedUpdate()
    {
        Move();
        GroundCheck();
    }

    IEnumerator Jump()
    {
        if (isGrounded)
        {
            spineRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            canJump = false;
            yield return new WaitForSeconds(1f);
            canJump = true;
        }
    }

    void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = transform.right * horizontalInput + transform.forward * verticalInput;
        moveDirection *= Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : speed;

        if (spineRigidbody != null)
        {
            Vector3 velocity = moveDirection;
            velocity.y = spineRigidbody.velocity.y;
            spineRigidbody.velocity = velocity;
        }
    }

    void GroundCheck()
    {
        Vector3 rayOrigin = transform.position + Vector3.up * 0.1f;
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, Vector3.down, out hit, groundRaycastDistance + 0.1f))
        {
            if (hit.collider.CompareTag("Ground"))
            {
                isGrounded = true;
                return;
            }
        }
        isGrounded = false;
    }
}
