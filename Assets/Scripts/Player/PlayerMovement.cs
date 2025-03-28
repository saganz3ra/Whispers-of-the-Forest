using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    bool forward, backward, left, right;
    bool grounded, jump;

    public Rigidbody rb;
    public Transform cameraTransform; // Referência à câmera

    public float speed, maxspeed, drag;
    public float rotationSpeed, jumpForce;
    public LayerMask ground;

    void Update()
    {
        HandleInput();
        CheckGrounded();
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
            Vector3 forwardDirection = cameraTransform.forward;
            Vector3 rightDirection = cameraTransform.right;
            
            // Mantemos apenas o movimento no plano horizontal
            forwardDirection.y = 0;
            rightDirection.y = 0;
            forwardDirection.Normalize();
            rightDirection.Normalize();

            if (forward) moveDirection += forwardDirection;
            if (backward) moveDirection -= forwardDirection;
            if (left) moveDirection -= rightDirection;
            if (right) moveDirection += rightDirection;
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
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z) / (1 + drag / 100) + new Vector3(0, rb.velocity.y, 0);
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
        grounded = Physics.Raycast(transform.position, Vector3.down, 1.1f, ground);
    }
}
