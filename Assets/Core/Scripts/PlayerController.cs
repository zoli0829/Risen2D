using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private PlayerControls controls;
    private Rigidbody2D rb;
    [SerializeField] SpriteRenderer playerSpriteRenderer;

    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    private Vector2 moveInput;

    private void Awake()
    {
        controls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        controls.Player.Enable();
        controls.Player.Move.performed += OnMove;
        controls.Player.Move.canceled += OnMove;
    }

    private void OnDisable()
    {
        controls.Player.Move.performed -= OnMove;
        controls.Player.Move.canceled -= OnMove;
        controls.Player.Disable();
    }

    private void OnMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        HandleMovementInput();
    }

    private void HandleMovementInput()
    {
        rb.linearVelocity = moveInput * moveSpeed;
        if (moveInput.x > 0)
        {
            // flip sprite to the right
            playerSpriteRenderer.flipX = false;
        }
        else if (moveInput.x < 0)
        {
            // flip sprite to the left
            playerSpriteRenderer.flipX = true;
        }
    }
}