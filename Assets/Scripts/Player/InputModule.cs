using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputModule : MonoBehaviour
{
    //PlayerController
    private PlayerController playerController;

    //Flash Light GameObject
    public GameObject flashlight;

    private void Awake()
    {
        //Get the Player Controller Component
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {

        //Check If Key (E) is Pressed AND if Player is in a staircase Trigger.
        if (Input.GetKeyDown(playerController.interactionKey) && playerController.isPlayerInStaircase)
        {
            for (int i = 0; i <= playerController.collisionModule.currentCollisions.Count - 1; i++)
            {
                //Check if Gameobject is a Staircase
                if (playerController.collisionModule.currentCollisions[i].tag == "Staircase")
                {

                    //This will move the player & Get the destination of the staircase
                    playerController.movementModule.SCMovement(playerController
                        .collisionModule.currentCollisions[i].GetComponent<StaircaseController>().destination);

                    //Break out of Loops (Found what we needed)
                    break;
                }
            }
        }

        if (Input.GetKeyDown(playerController.interactionKey) && playerController.isPlayerInElevator)
        {
            for (int i = 0; i <= playerController.collisionModule.currentCollisions.Count - 1; i++)
            {
                //Check if Gameobject is an Elevator
                if (playerController.collisionModule.currentCollisions[i].tag == "Elevator")
                {

                    //This will use the elevator through the Elevator Controller
                    playerController.collisionModule.currentCollisions[i].gameObject.GetComponent<ElevatorController>().UseElevator();

                    //Break out of Loops (Found what we needed)
                    break;
                }
            }
        }

        //Player is Interacting with Light Switch 
        if (Input.GetKeyDown(playerController.interactionKey) && playerController.isPlayerInLightswitch && playerController.gameController.isFuseBlown == false)
        {
            for (int i = 0; i <= playerController.collisionModule.currentCollisions.Count - 1; i++)
            {
                //Check if Gameobject is a LighSwitch
                if (playerController.collisionModule.currentCollisions[i].tag == "LightSwitch")
                {
                    //Set bool to opposite of what it currently is
                    playerController.collisionModule.currentCollisions[i].GetComponent<LightModule>().setLightState();

                    //Checks if Fuse will get blown or Lights will turn on
                    GameObject.FindGameObjectWithTag("LightController").GetComponent<LightController>().CheckLightSwitch();

                    //Break out of Loops (Found what we needed)
                    break;
                }
            }
        }

        //Player is Interacting with Fusebox
        if (Input.GetKeyDown(playerController.interactionKey) && playerController.isPlayerInFusebox)
        {
            //Loop through currentCollisions
            for (int i = 0; i <= playerController.collisionModule.currentCollisions.Count - 1; i++)
            {
                //Check if Gameobject is a Fusebox
                if (playerController.collisionModule.currentCollisions[i].tag == "Fusebox")
                {
                    //Get Fusebox Controller and Call FixFuse Function
                    playerController.collisionModule.currentCollisions[i].gameObject.GetComponent<FuseboxController>().FixFusebox();

                    //Break out of Loops (Found what we needed)
                    break;
                }
            }
        }

        //Player is Interacting with Generator Button
        if (Input.GetKeyDown(playerController.interactionKey) && playerController.isPlayerInGenerator)
        {
            for (int i = 0; i <= playerController.collisionModule.currentCollisions.Count - 1; i++)
            {
                //Check if Gameobject is a Staircase
                if (playerController.collisionModule.currentCollisions[i].tag == "Generator")
                {
                    playerController.collisionModule.currentCollisions[i].GetComponent<GeneratorController>().DisplayWindow();
                }
            }
        }


        //Below is Checking if Flash light is Enabled
        if (playerController.isFlashLightEnabled)
        {
            //Player moving the mouse to rotate the flashlight
            Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(flashlight.transform.position);

            //Get Angle of Flash Light
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            //Rotate Flash light
            flashlight.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        } else
        {
            //Flash Light Disabled
            flashlight.SetActive(false);
        }


        //Zoom Out to View the entire Level
        if (Input.GetKey(playerController.fullLevelViewKey) && !playerController.isPlayerWalking)
        {
            //Player is Holding Key && is NOT moving
            if (Vector3.Distance(playerController.cam.transform.position, playerController.startCamPosition) > 0.01)
            {
                playerController.cam.transform.position
                    = Vector3.Lerp(playerController.cam.transform.position, playerController.startCamPosition, playerController.camMoveOutSpeed * Time.deltaTime); ;
                playerController.cam.transform.position
                    = Vector3.MoveTowards(playerController.cam.transform.position, playerController.startCamPosition, playerController.camMoveOutSpeed * Time.deltaTime); ;
            }

            //Player is Viewing Full Level
            playerController.isViewingFullLevel = true;
            
        }
        else
        {
            //Check Distance
            if (Vector2.Distance(playerController.cam.transform.position, playerController.currentRoom.pos.transform.position) > 0.01)
            {
                playerController.cam.transform.position
                    = Vector3.Lerp(playerController.cam.transform.position, playerController.currentRoom.pos.transform.position, playerController.camMoveSpeed * Time.deltaTime);
                playerController.cam.transform.position
                    = Vector3.MoveTowards(playerController.cam.transform.position, playerController.currentRoom.pos.transform.position, playerController.camMoveSpeed * Time.deltaTime); ;
            }

            playerController.isViewingFullLevel = false;
        }

        
    }
}
