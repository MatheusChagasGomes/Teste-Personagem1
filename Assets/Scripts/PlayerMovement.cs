using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Controla a movimentação do personagem.
/// Inclui movimentação baseada na câmera, corrida, pulo duplo e alinhamento visual do personagem.
/// </summary>

public class PlayerMovement : MonoBehaviour
{
    [Header("Configurações")]
    public float walkSpeed = 5f;
    public float sprintSpeed = 9f;
    public float jumpForce = 7f;
    public int maxJumps = 2;

    [Header("Componentes")]
    public Rigidbody rb;
    public Transform visual;

    private PlayerInputActions inputActions;
    private Vector2 inputVector;
    private bool isSprinting;
    private int jumpCount;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActions.Player.Enable();

        inputActions.Player.Jump.performed += ctx => Jump();
        inputActions.Player.Sprint.performed += ctx => isSprinting = true;
        inputActions.Player.Sprint.canceled += ctx => isSprinting = false;
    }

    private void Update()
    {
        inputVector = inputActions.Player.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Transform cam = Camera.main.transform;

        Vector3 forward = cam.forward;
        Vector3 right = cam.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = forward * inputVector.y + right * inputVector.x;
        moveDirection.Normalize();

        float currentSpeed = isSprinting ? sprintSpeed : walkSpeed;

        rb.MovePosition(rb.position + moveDirection * currentSpeed * Time.fixedDeltaTime);

        if (moveDirection != Vector3.zero)
        {
            visual.forward = moveDirection;
        }
    }

    private void Jump()
    {
        if (jumpCount < maxJumps)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z); // Zera o Y para pulo consistente
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpCount++;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Considera que tocou o chão se o contato for de baixo pra cima
        if (collision.contacts[0].normal.y > 0.5f)
        {
            jumpCount = 0;
        }
    }
}