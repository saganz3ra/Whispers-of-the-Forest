using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    bool forward, backward, left, right;
    bool grounded, jump;

    public Rigidbody rb;
    public Transform cameraTransform;
    public Animator animator;

    public float speed = 10f;
    public float maxspeed = 5f;
    public float drag = 1f;
    public float rotationSpeed = 10f;
    public float jumpForce = 7f;

    [Header("Ground Check Settings")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.4f;
    public LayerMask ground;

    void Update()
    {
        HandleInput();
        CheckGrounded();
        UpdateAnimator();
    }

    void FixedUpdate()
    {
        HandleMovement();
        HandleRotation();
        LimitVelocity();
        HandleDrag();
        HandleJump();
    }

    void HandleInput()
    {
        forward = Input.GetKey(KeyCode.W);
        backward = Input.GetKey(KeyCode.S);
        left = Input.GetKey(KeyCode.A);
        right = Input.GetKey(KeyCode.D);

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            jump = true;
        }
    }

    void HandleMovement()
    {
        Vector3 moveDirection = Vector3.zero;

        if (cameraTransform != null)
        {
            Vector3 forwardDir = cameraTransform.forward;
            Vector3 rightDir = cameraTransform.right;

            forwardDir.y = 0;
            rightDir.y = 0;
            forwardDir.Normalize();
            rightDir.Normalize();

            if (forward) moveDirection += forwardDir;
            if (backward) moveDirection -= forwardDir;
            if (left) moveDirection -= rightDir;
            if (right) moveDirection += rightDir;
        }

        moveDirection.Normalize();

        if (moveDirection != Vector3.zero)
        {
            rb.AddForce(moveDirection * speed, ForceMode.Force);
        }
    }

    void HandleRotation()
    {
        Vector3 horizontalVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        if (horizontalVelocity.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(horizontalVelocity);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }

    void LimitVelocity()
    {
        Vector3 horizontalVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        if (horizontalVelocity.magnitude > maxspeed)
        {
            Vector3 limitedVelocity = horizontalVelocity.normalized * maxspeed;
            rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
        }
    }

    void HandleDrag()
    {
        Vector3 horizontalVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        horizontalVelocity = Vector3.Lerp(horizontalVelocity, Vector3.zero, drag * Time.fixedDeltaTime);
        rb.velocity = new Vector3(horizontalVelocity.x, rb.velocity.y, horizontalVelocity.z);
    }

    void HandleJump()
    {
        if (jump && grounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        jump = false;
    }

    void CheckGrounded()
    {
        grounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, ground);
        // Visualização opcional no editor
        Debug.DrawRay(groundCheck.position, Vector3.down * groundCheckRadius, grounded ? Color.green : Color.red);
    }

    void UpdateAnimator()
    {
        if (animator == null) return;

        float horizontalSpeed = new Vector3(rb.velocity.x, 0, rb.velocity.z).magnitude;
        animator.SetFloat("Speed", horizontalSpeed);

        // Quando não está no chão, ativa o pulo
        animator.SetBool("isJumping", !grounded);
    }
}
