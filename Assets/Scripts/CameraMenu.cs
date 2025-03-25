using UnityEngine;

public class CameraMenu : MonoBehaviour
{
    public Transform optionsPosition;  
    public Transform optionsRotation;  

    public Transform creditsPosition;  
    public Transform creditsRotation;  

    public float moveSpeed = 2f;

    private Vector3 startPosition;
    private Quaternion startRotation;
    private bool isMoving = false;
    private Vector3 targetPosition;
    private Quaternion targetRotation;

    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, moveSpeed * Time.deltaTime);

            // Parar o movimento quando estiver próximo do alvo
            if (Vector3.Distance(transform.position, targetPosition) < 0.05f &&
                Quaternion.Angle(transform.rotation, targetRotation) < 1f)
            {
                transform.position = targetPosition;
                transform.rotation = targetRotation;
                isMoving = false;
            }
        }
    }

    private void MoveCamera(Transform newTargetPosition, Transform newTargetRotation)
    {
        targetPosition = newTargetPosition.position;
        targetRotation = newTargetRotation.rotation;
        isMoving = true;
    }

    public void MoveToOptions()
    {
        MoveCamera(optionsPosition, optionsRotation);
    }

    public void MoveToCredits()
    {
        MoveCamera(creditsPosition, creditsRotation);
    }

    public void ResetCamera()
    {
        MoveCamera(new GameObject { transform = { position = startPosition, rotation = startRotation } }.transform, new GameObject { transform = { rotation = startRotation } }.transform);
    }
}
