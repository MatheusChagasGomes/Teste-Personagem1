using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Controla uma câmera em terceira pessoa com rotação horizontal baseada no movimento do mouse.
/// A câmera segue o alvo (Personagem) a uma certa distância e altura, permitindo rotação suave.
/// </summary>

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("Referências")]
    public Transform target;

    [Header("Sensibilidade")]
    public float horizontalSensitivity = 100f;

    [Header("Distância e Altura")]
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

        // Atualiza o ângulo horizontal
        yaw += lookX * horizontalSensitivity * Time.deltaTime;

        // Cria rotação no eixo Y (horizontal)
        Quaternion rotation = Quaternion.Euler(0f, yaw, 0f);

        // Calcula a posição e aplica a altura fixa
        Vector3 direction = new Vector3(0, 0, -distanceFromTarget);
        Vector3 offset = rotation * direction + Vector3.up * cameraHeight;

        // Aplica posição e rotação
        Camera.main.transform.position = target.position + offset;
        Camera.main.transform.rotation = rotation;
    }
}