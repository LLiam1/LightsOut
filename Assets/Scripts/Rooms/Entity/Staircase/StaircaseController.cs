using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaircaseController : MonoBehaviour
{
    //Staircase Destination
    public Vector3 destination;

    //GameObject Destination
    public GameObject scDestination;

    //Staircase goes Up
    public bool isUpStaircase;

    //Staircase goes down
    public bool isDownStaircase;

    //What is this bool?
    public bool isav = false;

    private void Start()
    {
        
        if (isUpStaircase)
        {
            //TODO: Check If Staircase is above (Possible no Room there or staircase)

            //Up Staircase
            destination = new Vector3(transform.position.x, transform.position.y + 13, transform.position.z);


        }
        else if (isDownStaircase)
        {
            //TODO: Check If Staircase is above (Possible no Room there or staircase)
            RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, Vector2.down, 10f, 1 << LayerMask.NameToLayer("Staircase"));

            //Down Staircase
            destination = new Vector3(transform.position.x, transform.position.y - 10, transform.position.z);
        }
        else
        {
            //This is not a staircase!
            return;
        }
    }
}


