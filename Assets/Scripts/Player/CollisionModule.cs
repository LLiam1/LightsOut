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

    private void Awake()
    {
        //Get the PController Component
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        //Check If Collisions is Empty
        if (!currentCollisions.Any())
        {
            playerController.gameController.buttonHelpMenu.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Player Enters Staircase Trigger
        if (collision.gameObject.tag == "Staircase")
        {
            //Set Bool Trigger to True
            playerController.isPlayerInStaircase = true;

            playerController.gameController.SetHelpMenuText("Press E to use Staircase!");
        }

        //Player Enters Switch Trigger
        if (collision.gameObject.tag == "LightSwitch")
        {
            //Set Bool Trigger to True
            playerController.isPlayerInLightswitch = true;

            playerController.gameController.SetHelpMenuText("Press Q to use Light Switch!");
        }

        //Player Enters Fusebox Trigger
        if (collision.gameObject.tag == "Fusebox")
        {
            //Set Bool Trigger to True
            playerController.isPlayerInFusebox = true;


            playerController.gameController.SetHelpMenuText("Press Z to use Fusebox!");
        }

        //Player enters Generator Trigger
        if (collision.gameObject.tag == "Generator")
        {
            playerController.isPlayerInGenerator = true;

            playerController.gameController.SetHelpMenuText("Press S to use Generator!");
        }

        if (collision.gameObject.tag == "Room")
        {
            playerController.currentRoom = collision.gameObject.GetComponent<RoomModule>();
        }

        //Do NOT add Rooms to collisions

        if (collision.gameObject.tag != "Room")
        {
            //Set the current Collision object
            currentCollisions.Add(collision.gameObject);
        }
    }




    private void OnTriggerExit2D(Collider2D collision)
    {
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
