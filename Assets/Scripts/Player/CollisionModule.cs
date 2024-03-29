using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using System.Linq;

public class CollisionModule : MonoBehaviour
{
    //Player Controller
    private PlayerController playerController;

    //Current Collision Object
    //public GameObject[] curretCollision;

    //List Because the Player can be in multiple Collisions at once
    public List<GameObject> currentCollisions = new List<GameObject>();

    public GameObject highlight;
    private GameObject thisHighlight;

    private void Awake()
    {
        //Get the PController Component
        playerController = GetComponent<PlayerController>();
    }

    private void Start()
    {
      thisHighlight = Instantiate(highlight);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (currentCollisions.Count >= 0 && collision.gameObject.tag != "Room")
        {
            if (thisHighlight != null)
            {
                thisHighlight.transform.position = collision.transform.position;
                thisHighlight.GetComponent<Light2D>().intensity = 3;
            }
        }

        //Player Enters Staircase Trigger
        if (collision.gameObject.tag == "Staircase")
        {
            //Set Bool Trigger to True
            playerController.isPlayerInStaircase = true;

        }

        if (collision.gameObject.tag == "Elevator")
        {
            //Set Bool Trigger to True
            playerController.isPlayerInElevator = true;

        }

        //Player Enters Switch Trigger
        if (collision.gameObject.tag == "LightSwitch")
        {
            //Set Bool Trigger to True
            playerController.isPlayerInLightswitch = true;

        }

        //Player Enters Fusebox Trigger
        if (collision.gameObject.tag == "Fusebox")
        {
            //Set Bool Trigger to True
            playerController.isPlayerInFusebox = true;

        }

        //Player enters Generator Trigger
        if (collision.gameObject.tag == "Generator")
        {
            playerController.isPlayerInGenerator = true;

        }

        if (collision.gameObject.tag == "Room")
        {
            playerController.currentRoom = collision.gameObject.GetComponent<RoomModule>();
        }

        if (collision.gameObject.tag != "Room")
        {
            //Set the current Collision object
            currentCollisions.Add(collision.gameObject);
        }
    }




    private void OnTriggerExit2D(Collider2D collision)
    {

        if (currentCollisions.Count >= 0 && collision.gameObject.tag != "Room")
        {
            thisHighlight.GetComponent<Light2D>().intensity = 0;
        }

        //Player Exits Staircase Trigger
        if (collision.gameObject.tag == "Staircase")
        {
            //Set Bool Trigger to False
            playerController.isPlayerInStaircase = false;

        }

        //Player Exits Switch Trigger
        if (collision.gameObject.tag == "LightSwitch")
        {
            //Set Bool Trigger to False
            playerController.isPlayerInLightswitch = false;

        }

        //Player Exits Fusebox Trigger
        if (collision.gameObject.tag == "Fusebox")
        {
            //Set bool Trigger to False
            playerController.isPlayerInFusebox = false;

        }

        if (collision.gameObject.tag == "Generator")
        {
            playerController.isPlayerInGenerator = false;

        }

        //Update Current Collision Object
        currentCollisions.Remove(collision.gameObject);
    }

}
