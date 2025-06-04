using UnityEngine;

public class FootstepController : MonoBehaviour
{
    [Header("Audio Setup")]
    public AudioSource footstepSource;
    public AudioClip grassClip;
    public AudioClip gravelClip;
    public AudioClip woodClip;
    public Transform groundCheck;

    [Header("Raycast Settings")]
    public LayerMask terrainMask;
    public float stepRate = 0.4f;
    private float stepTimer;

    void Update()
    {
        if (IsMoving() && stepTimer <= 0f)
        {
            PlayFootstep();
            stepTimer = stepRate;
        }

        stepTimer -= Time.deltaTime;
    }

    bool IsMoving()
    {
        return Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0;
    }

    void PlayFootstep()
    {
        if (footstepSource == null)
        {
            Debug.LogWarning("Footstep AudioSource não atribuído!");
            return;
        }

        if (groundCheck == null)
        {
            Debug.LogWarning("GroundCheck não atribuído!");
            return;
        }

        Vector3 origin = groundCheck.position + Vector3.up * 0.1f;
        float distance = 1f;

        Debug.DrawRay(origin, Vector3.down * distance, Color.red, 1f);

        if (Physics.Raycast(origin, Vector3.down, out RaycastHit hit, distance, terrainMask))
        {
            Debug.Log("Raycast acertou: " + hit.collider.name + " | Tag: " + hit.collider.tag);

            switch (hit.collider.tag)
            {
                case "Grass":
                    footstepSource.PlayOneShot(grassClip);
                    break;
                case "Gravel":
                    footstepSource.PlayOneShot(gravelClip);
                    break;
                case "Wood":
                    footstepSource.PlayOneShot(woodClip);
                    break;
                default:
                    Debug.LogWarning("Terreno com tag desconhecida: " + hit.collider.tag);
                    break;
            }
        }
        else
        {
            Debug.LogWarning("Raycast ainda não atingiu o chão.");
        }
    }
}
