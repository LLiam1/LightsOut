using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseboxController : MonoBehaviour
{
    //Get GameController
    private GameController gameController;

    private bool isFuseOpen = false;

    public GameObject fuseBoxTaskUI;

    private void Start()
    {
        //Get Game Controller
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    //Fix Fuse Function
    public void FixFusebox()
    {
        
        if (gameController.isFuseBlown)
        {
            if (isFuseOpen)
            {
                //If Open close it
                fuseBoxTaskUI.GetComponent<FuseBoxScript>().enabled = false;
                fuseBoxTaskUI.SetActive(true);
            }
            else
            {
                //Spawn Task Window
                fuseBoxTaskUI.GetComponent<FuseBoxScript>().enabled = true;
                fuseBoxTaskUI.SetActive(true);
            }
        }
    }

    private void Update()
    {
        if(!gameController.isFuseBlown)
        {
            fuseBoxTaskUI.SetActive(false);
        }

    }
}
