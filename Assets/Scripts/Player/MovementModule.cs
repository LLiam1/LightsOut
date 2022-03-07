using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementModule : MonoBehaviour
{
    private PlayerController playerController;

    private void Awake()
    {
        //Get the PController Component
        playerController = GetComponent<PlayerController>();
    }


    //Player Movement: Takes a Vector3 Position
    public void Movement(Vector3 move)
    {
        playerController.rb.MovePosition(transform.position + move);
    }

    //Staircase Movement
    public void SCMovement(Vector3 move)
    {
        //Moves the Player to Staircase Destination
        transform.position = move;
    }

}
