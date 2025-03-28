using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 2, -4);
    public float sensitivity = 3.0f;
    public float rotationYMin = -20f, rotationYMax = 60f;

    private float rotationX = 0f;
    private float rotationY = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        // Se o mouse estiver invertido, mude o sinal dos eixos:
        rotationX += Input.GetAxis("Mouse X") * sensitivity; // Inverter se necessário (-Input.GetAxis)
        rotationY += Input.GetAxis("Mouse Y") * sensitivity; // Removido o "-" para corrigir inversão

        rotationY = Mathf.Clamp(rotationY, rotationYMin, rotationYMax);
    }

    void LateUpdate()
    {
        if (target != null)
        {
            Quaternion rotation = Quaternion.Euler(rotationY, rotationX, 0);
            Vector3 newPosition = target.position + rotation * offset;
            transform.position = newPosition;
            transform.LookAt(target.position);
        }
    }
}
