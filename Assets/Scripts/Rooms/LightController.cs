using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    //Get Game Controller
    private GameController gameController;

    //Array of Light Switches
    //public GameObject[] lightSwitches;
    public List<GameObject> lightSwitches = new List<GameObject>();

    //Array of Current LightSwitches
    public List<GameObject> currentLightSwitches = new List<GameObject>();

    //Array of Lights
    // public GameObject[] lights;
    public List<GameObject> lights = new List<GameObject>();

    //Current Amount of Lights Active
    public int lightActiveCount = 0;

    //Maximium Lights Allowed Active at Once!
    public const int MAX_LIGHTS_ACITVE = 3;

    //Float Target Light Intensity
    public const float TARGET_LIGHT_INTENSITY = 0.29f;

    //Increase Light Intensity
    public float lightIncreaseAmount = 0.75f;

    //Light Descrease Intensity
    public float lightDecreaseAmount = 0.05f;

    //Check If Lights assigned
    private bool isLightAssigned = true;

    //Percentage Change the light goes out
    private int percLightTriggers = 25;

    private void Start()
    {
        //Get Game Controller
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    private void Update()
    {
        //Get the count of Active Lights
        lightActiveCount = GetLightCount();

        //Check if Lights Are Assigned.
        if (isLightAssigned == false)
        {
            for (int i = 0; i <= lightSwitches.Count - 1; i++)
            {
                //Get Random Int
                int rand = Random.Range(0, lights.Count - 1);

                //Set Random Light to LightSwitch
                lightSwitches[i].GetComponent<LightModule>().connectedLight = lights[rand].gameObject;


                //Remove Both Objects from the List
                lightSwitches.Remove(lightSwitches[i].gameObject);
                lights.Remove(lights[rand].gameObject);
            }
        }
    }

    public void SetupLights()
    {
        //Get All Light Switches (Used for Random Assignment of Light/Switch) (Eventually Removed From List)
        foreach (GameObject ls in GameObject.FindGameObjectsWithTag("LightSwitch"))
        {
            lightSwitches.Add(ls);
        }

        //Get All Lights
        foreach (GameObject l in GameObject.FindGameObjectsWithTag("Light"))
        {
            lights.Add(l);
        }

        //Disable all Light Switches on Start
        for (int i = 0; i <= lightSwitches.Count - 1; i++)
        {
            //Turn all Lights Off
            lightSwitches[i].GetComponent<LightModule>().isLightOn = false;
        }

        //Assign Light to Random Switch.
        isLightAssigned = false;

        //Get Current Switches (Never Removed from List)
        foreach (GameObject ls in GameObject.FindGameObjectsWithTag("LightSwitch"))
        {
            currentLightSwitches.Add(ls);
        }
    }

    public void CheckLightSwitch()
    {
        //Check if Fuse is Blown
        if (lightActiveCount > MAX_LIGHTS_ACITVE && Random.Range(0, 100) < percLightTriggers)
        {

            //Blown Fuse! Loop Through all Lights & Turn them Off
            for (int i = 0; i <= currentLightSwitches.Count - 1; i++)
            {
                //Turn all Lights Off
                currentLightSwitches[i].GetComponent<LightModule>().setLightState(false);
            }

            //Activate Blown Fuse
            gameController.isFuseBlown = true;
        }
    }

    //Get Number of Active Lights
    private int GetLightCount()
    {
        //Light Counter
        int count = 0;

        //Loop through Array
        for (int i = 0; i <= currentLightSwitches.Count - 1; i++)
        {
            //Check if Light is Active
            if (currentLightSwitches[i].GetComponent<LightModule>().isLightOn)
            {
                //Add to count
                count++;
            }
        }
        //Return Count
        return count;
    }
}
