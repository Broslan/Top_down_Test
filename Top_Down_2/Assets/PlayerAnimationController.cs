using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    public void MousLookAround()
    {

    }
    void Update()
    {
        
    }
}
