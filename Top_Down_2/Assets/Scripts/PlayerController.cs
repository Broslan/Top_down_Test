using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //For Animation
    [Header("Animation Handler")]
    [Tooltip("Choose Animation of weapon (Rifle)")]
    [SerializeField] internal Animator gunAnimator;
    [Tooltip("Rotate AimAxis")]
    [SerializeField] internal Transform aimAxis;
    [SerializeField] internal Transform shootFrom;
    internal float lookAngle;
    internal Camera cam;
    internal Vector2 lastMovmentVector; //for animation
    internal Animator anim; //main model Animator
    internal Vector2 mousePos;
    internal bool isMoving = false;
    [Space]


    //Player scripts handler
    [Header("Script Handler")]
    [SerializeField]
    internal PlayerAnimationController playerAnimationController;
    [SerializeField] 
    internal PlayerMovmentController playerMovmentController;
    //[SerializeField]
    //internal FieldOfView fieldOfViev;
    [SerializeField]
    internal PlayerShooting playerShooting;

    //private GridMap<int> grid;
    void Awake()
    {
        anim = GetComponent<Animator>();
        cam = Camera.main;
        playerAnimationController = GetComponent<PlayerAnimationController>();
        playerMovmentController = GetComponent<PlayerMovmentController>();
        //grid = new GridMap<int>(10, 10, 1f * .99f, new Vector3(10,10));
        //grid.SetValue(5, 5, 10);
    }


    private void Update()
    {
        playerAnimationController.MouseLookAnimationHandler();
        //fieldOfViev.SetOrigin(playerMovmentController.rb.position);
        //if (Mouse.current.rightButton.wasPressedThisFrame)
        //{
        //    Debug.Log(grid.GetValue(cam.ScreenToWorldPoint(Mouse.current.position.ReadValue())));
        //}
    }

    public void Shoot(InputAction.CallbackContext context)
    {
        gunAnimator.SetBool("Shoot", context.performed);
        if (context.performed == true)
            playerShooting.Shoot(aimAxis.position, cam.ScreenToWorldPoint(Mouse.current.position.ReadValue()));
        //if (context.performed == true)
        //    grid.SetValue(cam.ScreenToWorldPoint(Mouse.current.position.ReadValue()), grid.GetValue(cam.ScreenToWorldPoint(Mouse.current.position.ReadValue())) + 1);
    }

    private void FixedUpdate()
    {
        playerMovmentController.Movment();
    }

    
}


