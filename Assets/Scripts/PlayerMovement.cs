using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal.Internal;

public class PlayerMovement : MonoBehaviour
{
    bool left, forward, backward, right;
    bool grounded, jump;

    public Rigidbody rb;

    public float speed, maxspeed, drag;
    public float rotationSpeed, jumpForce;
    public LayerMask ground;

    void Update()
    {
        HandleInput();
        LimitVelocity();
        HandleDrag();
        CheckGrounded();
    }

    void FixedUpdate()
    {
         HandleRotation();

        if (left)
        {
            rb.AddForce(Vector3.left * speed);
            left = false;
        }
        if (forward)
        {
            rb.AddForce(Vector3.forward * speed);
            forward = false;
        }
        if (backward)
        {
            rb.AddForce(Vector3.back * speed);
            backward = false;
        }
        if (right)
        {
            rb.AddForce(Vector3.right * speed);
            right = false;
        }

        if (jump && grounded)
        {
            transform.position += Vector3.up * .1f;
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.y);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jump = false;
        }
    }

    void HandleInput()
    {
        if (Input.GetKey(KeyCode.A))
            left = true;
        if (Input.GetKey(KeyCode.W))
            forward = true;
        if (Input.GetKey(KeyCode.S))
            backward = true;
        if (Input.GetKey(KeyCode.D))
            right = true;
        if (Input.GetKey(KeyCode.Space) && grounded)
            jump = true;
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

    void HandleRotation()
    {
        if ((new Vector2(rb.velocity.x, rb.velocity.z)).magnitude > .1f)
        {
            Vector3 horizontalDir = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            Quaternion rotation = Quaternion.LookRotation(horizontalDir, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed);
        }
    }

    void CheckGrounded()
    {
        grounded = Physics.Raycast(transform.position + Vector3.up * .1f, Vector3.down, .2f, ground);
    }
}
