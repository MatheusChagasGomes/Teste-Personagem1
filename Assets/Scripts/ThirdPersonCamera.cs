using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("Refer�ncias")]
    public Transform target;

    [Header("Sensibilidade")]
    public Vector2 sensitivity = new Vector2(100f, 0.5f);

    [Header("Dist�ncia e Altura")]
    public float distanceFromTarget = 5f;
    public float cameraHeight = 2f;

    [Header("Limites Verticais")]
    public float verticalMin = -40f;
    public float verticalMax = 80f;

    [Header("Input")]
    public InputActionReference lookAction;

    private float yaw;   // Rota��o horizontal
    private float pitch; // Rota��o vertical

    void Start()
    {
        if (lookAction != null)
        {
            lookAction.action.Enable();
        }
    }

    void LateUpdate()
    {
        if (target == null || lookAction == null) return;

        // Leitura do input do mouse
        Vector2 look = lookAction.action.ReadValue<Vector2>();

        // Ajuste de rota��o com base na sensibilidade
        yaw += look.x * sensitivity.x * Time.deltaTime;
        pitch -= look.y * sensitivity.y * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, verticalMin, verticalMax);

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);

        Vector3 direction = new Vector3(0, 0, -distanceFromTarget);
        Vector3 offset = rotation * direction + Vector3.up * cameraHeight;

        // Aplica posi��o e rota��o na c�mera principal
        Camera.main.transform.position = target.position + offset;
        Camera.main.transform.rotation = rotation;

        // Faz a c�mera olhar para o personagem (ligeiramente acima do piv�)
        Camera.main.transform.LookAt(target.position + Vector3.up * cameraHeight);
    }
}