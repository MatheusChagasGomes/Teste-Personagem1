using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Controla uma c�mera em terceira pessoa com rota��o horizontal baseada no movimento do mouse.
/// A c�mera segue o alvo (Personagem) a uma certa dist�ncia e altura, permitindo rota��o suave.
/// </summary>

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("Refer�ncias")]
    public Transform target;

    [Header("Sensibilidade")]
    public float horizontalSensitivity = 100f;

    [Header("Dist�ncia e Altura")]
    public float distanceFromTarget = 5f;
    public float cameraHeight = 2f;

    [Header("Input")]
    public InputActionReference lookAction;

    private float yaw;

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

        // Leitura do eixo X (horizontal)
        float lookX = lookAction.action.ReadValue<Vector2>().x;

        // Atualiza o �ngulo horizontal
        yaw += lookX * horizontalSensitivity * Time.deltaTime;

        // Cria rota��o no eixo Y (horizontal)
        Quaternion rotation = Quaternion.Euler(0f, yaw, 0f);

        // Calcula a posi��o e aplica a altura fixa
        Vector3 direction = new Vector3(0, 0, -distanceFromTarget);
        Vector3 offset = rotation * direction + Vector3.up * cameraHeight;

        // Aplica posi��o e rota��o
        Camera.main.transform.position = target.position + offset;
        Camera.main.transform.rotation = rotation;
    }
}