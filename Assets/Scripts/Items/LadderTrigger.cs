using UnityEngine;

public class LadderTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement movement = other.GetComponent<PlayerMovement>();
            if (movement != null)
            {
                movement.isClimbing = true;
                movement.rb.velocity = Vector3.zero;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement movement = other.GetComponent<PlayerMovement>();
            if (movement != null)
            {
                movement.isClimbing = false;
            }
        }
    }
}
