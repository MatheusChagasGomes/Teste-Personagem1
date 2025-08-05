using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Configurações")]
    public float moveSpeed = 5f;
    public float jumpForce = 7f;

    [Header("Componentes")]
    public Rigidbody rb;
    public Transform visual;

    private PlayerInputActions inputActions;
    private Vector2 inputVector;
    private bool isGrounded;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActions.Player.Enable();
        inputActions.Player.Jump.performed += ctx => Jump();
    }

    private void Update()
    {
        inputVector = inputActions.Player.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        // Movimento
        Vector3 move = new Vector3(inputVector.x, 0f, inputVector.y);
        rb.MovePosition(rb.position + move * moveSpeed * Time.fixedDeltaTime);

        // Rotação do visual na direção do movimento
        if (move != Vector3.zero)
        {
            visual.forward = move;
        }
    }

    private void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Detecta se tocou o chão
        if (collision.contacts[0].normal.y > 0.5f)
        {
            isGrounded = true;
        }
    }
}