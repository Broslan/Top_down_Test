using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class CameraPositionController : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] Transform player;
    [SerializeField] float treshhold;
    
    void Update()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector3 targetPos = (player.position + mousePos) / 2f;
        targetPos.x = Mathf.Clamp(targetPos.x, -treshhold + player.position.x, treshhold + player.position.x);
        targetPos.y = Mathf.Clamp(targetPos.y, -treshhold + player.position.y, treshhold + player.position.y);
        this.transform.position = targetPos;
    }
}
