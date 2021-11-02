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


    void Awake()
    {
        anim = GetComponent<Animator>();
        cam = Camera.main;
        playerAnimationController = GetComponent<PlayerAnimationController>();
        playerMovmentController = GetComponent<PlayerMovmentController>();
    }

    
    

   

    private void Update()
    {
        playerAnimationController.MouseLookAnimationHandler();
        //fieldOfViev.SetOrigin(playerMovmentController.rb.position);
    }

    public void Shoot(InputAction.CallbackContext context)
    {
        gunAnimator.SetBool("Shoot", context.performed);
        if (context.performed == true)
            playerShooting.Shoot(aimAxis.position, cam.ScreenToWorldPoint(Mouse.current.position.ReadValue()));
    }

    private void FixedUpdate()
    {
        playerMovmentController.Movment();
    }

    
}
