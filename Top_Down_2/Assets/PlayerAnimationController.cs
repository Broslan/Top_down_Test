using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimationController : MonoBehaviour
{
    private PlayerController playerController;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    public void MouseLookAnimationHandler()
    {
        playerController.mousePos = playerController.cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 lookDir = playerController.mousePos - playerController.playerMovmentController.rb.position;
        float lookAngle1 = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        //playerController.fieldOfViev.SetAimDirection(lookAngle1);
        playerController.aimAxis.rotation = Quaternion.Euler(0, 0, lookAngle1);
        playerController.lookAngle = 180f - lookAngle1;
        if (playerController.lookAngle > 0 && playerController.lookAngle < 90 || playerController.lookAngle > 270 && playerController.lookAngle < 360)
        {
            playerController.aimAxis.localScale = new Vector3(1, -1, 1);
        }
        else
        {
            playerController.aimAxis.localScale = new Vector3(1, 1, 1);
        }
        playerController.isMoving = playerController.playerMovmentController.movmentVector.x != 0 || playerController.playerMovmentController.movmentVector.y != 0;
        playerController.anim.SetFloat("lookAngle", playerController.lookAngle);
        playerController.anim.SetBool("isMoving", playerController.isMoving);
        playerController.anim.SetFloat("horizontalMov", playerController.lastMovmentVector.x);
        playerController.anim.SetFloat("verticalMov", playerController.lastMovmentVector.y);
    }
}
