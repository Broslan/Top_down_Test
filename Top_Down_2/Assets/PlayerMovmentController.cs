using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovmentController : MonoBehaviour
{
    private PlayerController playerController;


    // For Movment
    [Header("Movment Var")]
    [Tooltip("How Fast Character Move")]
    [SerializeField]
    internal float movmentSpeed = 5f;
    [Tooltip("How Far Character Dash")]
    [SerializeField]
    internal float dashDistanse = 4f;
    [Tooltip("Through what cant Dash")]
    [SerializeField]
    LayerMask dashObstacleMask;
    [Space]
    internal Vector2 movmentVector;
    internal bool isDashing = false;
    internal Rigidbody2D rb;
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Movment()
    {
        rb.velocity = movmentVector * movmentSpeed;

        if (isDashing)
        {
            Vector2 dashPos = rb.position + movmentVector * dashDistanse;

            RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, movmentVector, dashDistanse, dashObstacleMask);

            if (raycastHit2D.collider != null)
            {
                dashPos = raycastHit2D.point;
            }

            rb.MovePosition(dashPos);
            isDashing = false;
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isDashing = true;
        }
    }

    public void MovmentInput(InputAction.CallbackContext context)
    {
        movmentVector = context.ReadValue<Vector2>();
        if (context.performed)
        {
            playerController.lastMovmentVector = context.ReadValue<Vector2>();
        }
    }
}
