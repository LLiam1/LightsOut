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
        if (Input.GetKeyDown(playerController.staircaseIntKey) && playerController.isPlayerInStaircase)
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

        //Player is Interacting with Light Switch 
        if (Input.GetKeyDown(playerController.lightIntKey) && playerController.isPlayerInLightswitch && playerController.gameController.isFuseBlown == false)
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
        if (Input.GetKeyDown(playerController.fixFuseKey) && playerController.isPlayerInFusebox)
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
        if (Input.GetKeyDown(playerController.genIntKey) && playerController.isPlayerInGenerator)
        {
            for (int i = 0; i <= playerController.collisionModule.currentCollisions.Count - 1; i++)
            {
                //Check if Gameobject is a Staircase
                if (playerController.collisionModule.currentCollisions[i].tag == "Generator")
                {
                    //Check if Popup exists
                    if (playerController.collisionModule.currentCollisions[i].GetComponent<GeneratorController>().currentPopup == null)
                    {


                        if (playerController.collisionModule.currentCollisions[i].GetComponent<GeneratorController>().isGeneratorActive == false)
                        {
                            //Display Window
                            playerController.collisionModule.currentCollisions[i].GetComponent<GeneratorController>().DisplayPopupWindow();


                        }
                    }
                }
            }
        }


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
            flashlight.SetActive(false);
        }

    }
}
