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
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().isPlayerInFusebox)
            {
                fuseBoxTaskUI.SetActive(true);
            }
        }
    }

    private void Update()
    {
        if (!gameController.isFuseBlown || !GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().isPlayerInFusebox)
        {
            fuseBoxTaskUI.SetActive(false);
        }

    }
}
