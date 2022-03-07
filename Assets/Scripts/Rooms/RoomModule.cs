using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class RoomModule : MonoBehaviour
{
    //This Rooms background Sprite
    public GameObject background;

    //This Rooms Current Light Object
    public GameObject currentLight;

    //Switch Connected Light
    public GameObject switchConnLight;

    //Light Status
    public bool isLightOn = false;

    //Entry Room
    public bool isEntryRoom, isElevatorRoom, isFuseRoom, isTrapRoom, isGenerator;

    //Array of Neightbors
    public List<RoomModule> theseNeighbors = new List<RoomModule>();

    public int X, Y;


    private void Start()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 10);

        // If it hits something...
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "Room")
            {
                theseNeighbors.Add(hit.collider.gameObject.GetComponent<RoomModule>());
            }
        }
    }

    private void Update()
    {
        //Set Light Intensity
        if (isLightOn)
        {
            currentLight.GetComponent<Light2D>().intensity = 3.25f;
        } else
        {
            currentLight.GetComponent<Light2D>().intensity = 0f;
        }

    }

    //Use this Func to get Status of Current Room Light
    public bool getStatusCurrentLight()
    {
        return currentLight.GetComponent<LightModule>().isLightOn;
    }

    //Func to get Status of Connection Room Light
    public bool getStatusConnectionLight()
    {
        //Return Status
        return switchConnLight.GetComponent<LightModule>().isLightOn; ;
    }


    //Use this Func to Set the Light Switch in this room to a specific Light Object
    public void setConnectionLight(GameObject _switchConnLight)
    {
        switchConnLight = _switchConnLight;

        //Set the Connected Light in the Light Module Class
        switchConnLight.GetComponent<LightModule>().connectedLight = switchConnLight;
    }


    // Use this Func to get the Light Switch this Room Switch is Attached too
    public GameObject getConnectionLight()
    {
        return switchConnLight;
    }

    public void SpawnElevator(GameObject highestYRoom)
    {
        throw new NotImplementedException();
    }

    public void SpawnFusebox(GameObject lowestYRoom)
    {
        throw new NotImplementedException();
    }

    public void SpawnGeneratorButton(GameObject gameObject)
    {
        throw new NotImplementedException();
    }

    public Vector3 CurrentPos()
    {

        Vector3 pos = new Vector3(transform.position.x, transform.position.y  + 3.05f, 0);

        return pos;
    }
}
