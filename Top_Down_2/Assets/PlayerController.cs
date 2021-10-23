 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float movmentSpeed = 5f;
    private Vector2 movmentVector;
    private bool isDashing = false;
    [SerializeField] private float dashDistanse = 4f;
    [SerializeField] LayerMask dashObstacleMask;
    [SerializeField] private Vector2 lastMovmentVector;
    private Animator anim;
    private bool isMoving = false;
    private Vector2 mousePos;
    private Camera cam;
    [SerializeField] private float lookAngle;
    [SerializeField] private Transform aimAxis;
    [SerializeField] private Animator gunAnimator;

    //Player scripts handler
    [SerializeField] PlayerAnimationController playerAnimationController;
    [SerializeField] PlayerMovmentController playerMovmentController;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        cam = Camera.main;
        playerAnimationController = GetComponent<PlayerAnimationController>();
        playerMovmentController = GetComponent<PlayerMovmentController>();
    }

    
    

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isDashing = true;
        }
    }

    public void Movment(InputAction.CallbackContext context)
    {
        movmentVector = context.ReadValue<Vector2>();
        if (context.performed)
        {
            lastMovmentVector = context.ReadValue<Vector2>();
        }
    }

    private void Update()
    {
        //MovmentAnimationHandler();
        MouseLookAnimationHandler();

    }

    public void Shoot(InputAction.CallbackContext context)
    {
        gunAnimator.SetBool("Shoot", context.performed);
    }
    private void MouseLookAnimationHandler()
    {
        mousePos = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 lookDir = mousePos - rb.position;
        float lookAngle1 = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        aimAxis.rotation = Quaternion.Euler(0, 0, lookAngle1);
        lookAngle = 180f - lookAngle1;
        if(lookAngle > 0 && lookAngle < 90 || lookAngle > 270 && lookAngle < 360)
        {
            aimAxis.localScale = new Vector3(1, -1, 1);
        }
        else
        {
            aimAxis.localScale = new Vector3(1, 1, 1);
        }
        isMoving = movmentVector.x != 0 || movmentVector.y != 0;
        anim.SetFloat("lookAngle", lookAngle);
        anim.SetBool("isMoving", isMoving);
        anim.SetFloat("horizontalMov", lastMovmentVector.x);
        anim.SetFloat("verticalMov", lastMovmentVector.y);
    }


    private void FixedUpdate()
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

    //void MovmentAnimationHandler()
    //{
    //    isMoving = movmentVector.x != 0 || movmentVector.y != 0;
    //    anim.SetBool("isMoving", isMoving);
    //    anim.SetFloat("horizontalMov", lastMovmentVector.x);
    //    anim.SetFloat("verticalMov", lastMovmentVector.y);
    //}
}
