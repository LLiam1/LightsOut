using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightModule : MonoBehaviour
{
    //Light Connected to Switch
    public GameObject connectedLight;

    //Status of Light Switch
    public bool isLightOn = false;

    public GameObject interactKeyDisplay;


    //Set the Light Status Opposite when Interated with
    public void setLightState(bool _state)
    {
        isLightOn = _state;

        connectedLight.transform.parent.gameObject.GetComponent<RoomModule>().isLightOn = isLightOn;
    }

    public void setLightState()
    {
        isLightOn = !isLightOn;

        connectedLight.transform.parent.gameObject.GetComponent<RoomModule>().isLightOn = isLightOn;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //TODO: Display What Key to Press
            //Instantiate(interactButton, transform.position, Quaternion.identity, transform);

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //TODO: Destroy Interact Key Display
            //Instantiate(interactButton, transform.position, Quaternion.identity, transform);

        }
    }

    public GameObject getConnectedLight()
    {
        return connectedLight;
    }
}
